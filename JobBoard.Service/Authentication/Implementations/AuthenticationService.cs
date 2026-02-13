using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hangfire;
using JobBoard.Core.Helpers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace JobBoard.Service.Authentication.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		#region Fields
		private readonly JwtSettings _jwtSettings;
		private readonly ITokenHelper _tokenHelper;
		private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly appDbContext _appDbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string host = "http://localhost:5173";
		private readonly IUrlHelper _urlHelper;

		private readonly IBackgroundJobClient _backgroundJobClient;
		#endregion

		#region Constructors
		public AuthenticationService(JwtSettings jwtSettings,
									 ITokenHelper tokenHelper,
									 IUserRefreshTokenRepository userRefreshTokenRepository,
									 UserManager<User> userManager,
									 IEmailService emailService,
									 appDbContext appDbContext,
									 IHttpContextAccessor httpContextAccessor,
									 IUrlHelper urlHelper,
									 IBackgroundJobClient backgroundJobClient


									)
		{
			_jwtSettings = jwtSettings;
			_tokenHelper = tokenHelper;
			_userRefreshTokenRepository = userRefreshTokenRepository;
			_userManager = userManager;
			_emailService = emailService;
			_appDbContext = appDbContext;
			_httpContextAccessor = httpContextAccessor;
			_urlHelper = urlHelper;
			_backgroundJobClient = backgroundJobClient;
		}

		#endregion

		#region Methods

		private async Task<string> _GenerateAccessTokenAsync(User user)
		{
			var userRoles = await _userManager.GetRolesAsync(user);

			var claims = await _GetUserClaimsAsync(user, userRoles.ToList());

			var jwtToken = new JwtSecurityToken(
						_jwtSettings.Issuer,
						_jwtSettings.Audience,
						 claims,
						expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccesTokenExpirationDuration),
						signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256));

			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

			return accessToken;
		}
		private string _GenerateRefreshToken()
		{
			var RandomNumber = new byte[32];
			var generator = RandomNumberGenerator.Create();

			generator.GetBytes(RandomNumber);

			return Convert.ToBase64String(RandomNumber);
		}
		private async Task<List<Claim>> _GetUserClaimsAsync(User user, List<string>? roles = null)
		{

			var userClaims = await _userManager.GetClaimsAsync(user);

			var claims = new List<Claim>()
			{
				new Claim(JwtClaimTypes.UserId, user.Id.ToString()),
				new Claim(JwtClaimTypes.Username, user.UserName ?? "Unknown"),
				new Claim(JwtClaimTypes.Email, user.Email ?? "Unknown"),
				new Claim(JwtClaimTypes.FirstName, user.FirstName),
				new Claim(JwtClaimTypes.LastName, user.LastName),

			};

			if (roles != null)
			{
				foreach (var role in roles)
				{
					claims.Add(new Claim(JwtClaimTypes.Role, role));
				}
			}


			claims.AddRange(userClaims);

			return claims;

		}


		public async Task<AuthResponse> GenerateUserToken(User user)
		{
			// Create access token

			var accessToken = await _GenerateAccessTokenAsync(user);

			// Generate and save user refresh token

			var refreshToken = new RefreshTokenResponse
			{
				UserId = user.Id,
				RefreshToken = _GenerateRefreshToken(), // refresh token value 
				ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDuration)
			};

			// check if user has an active refresh token

			var oldUserRefreshToken = await _userRefreshTokenRepository.GetTableAsNoTracking()
									.FirstOrDefaultAsync(x => x.UserId == user.Id &&
															x.RevokedOn == null);

			if (!(oldUserRefreshToken == null) && oldUserRefreshToken.IsActive)
			{
				oldUserRefreshToken.RevokedOn = DateTime.UtcNow;
				await _userRefreshTokenRepository.UpdateAsync(oldUserRefreshToken);
			}

			var userRefreshToken = new UserRefreshToken
			{
				UserId = user.Id,
				RefreshToken = refreshToken.RefreshToken,
				AccessToken = accessToken,
				CreatedOn = DateTime.UtcNow,
				ExpiresOn = refreshToken.ExpirationDate

			};

			await _userRefreshTokenRepository.AddAsync(userRefreshToken);

			var roles = await _userManager.GetRolesAsync(user);

			// return response of access and refresh tokens 
			return new AuthResponse
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				UserRoles = roles.ToArray()

			};

		}

		public async Task<AuthResponse> GetRefreshToken(string refreshToken)
		{

			var userrefreshToken = await _userRefreshTokenRepository.GetTableAsTracking()
									.Include(rf => rf.User)
									.FirstOrDefaultAsync(x => x.RefreshToken.Equals(refreshToken));

			if (userrefreshToken is null)
				throw new SecurityTokenException("Invalid Token Info");

			if (!userrefreshToken.IsActive)
				throw new SecurityTokenException("Refresh token Is Expired");

			var newAccessToken = await _GenerateAccessTokenAsync(userrefreshToken.User);

			// Update Access token
			userrefreshToken.AccessToken = newAccessToken;
			await _userRefreshTokenRepository.UpdateAsync(userrefreshToken);

			// return AuthResponse 
			return new AuthResponse
			{
				AccessToken = newAccessToken,
				RefreshToken = new RefreshTokenResponse
				{
					UserId = userrefreshToken.UserId,
					RefreshToken = userrefreshToken.RefreshToken,
					ExpirationDate = userrefreshToken.ExpiresOn
				}
			};

		}

		public JwtSecurityToken ReadJwtToken(string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken))
				throw new ArgumentNullException(nameof(accessToken));

			var handler = new JwtSecurityTokenHandler();
			return handler.ReadJwtToken(accessToken);

		}

		public async Task<string> SendResetPasswordAsync(string email)
		{

			var user = await _userManager.FindByEmailAsync(email);
			if (user is null || user.IsDeleted) return "UserNotFound";


			var bytes = new byte[4];
			RandomNumberGenerator.Fill(bytes);
			var randomCode = BitConverter.ToUInt32(bytes, 0) % 1000000;
			user.Code = randomCode.ToString("D6");

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) return "UpdateFailed";


			try
			{
				_backgroundJobClient.Enqueue<IEmailService>(emailService =>
					emailService.SendEmailAsync(email, user.FullName, Util.FormatVerificationMessage(user.Code), "Reset Password Code"));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to enqueue reset code email");
			}

			return "Success";
		}
		private async Task<string> GenerateResetPasswordTokenAsync(User user)
		{
			var claims = await _GetUserClaimsAsync(user);

			var jti = Guid.NewGuid().ToString("N").Substring(0, 10);
			user.Jti = jti;
			user.JtiExp = false;

			await _userManager.UpdateAsync(user);

			claims.Add(new Claim(JwtClaimTypes.Jti, jti));

			var jwtToken = new JwtSecurityToken(
						_jwtSettings.Issuer,
						_jwtSettings.Audience,
						 claims,
						expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccesTokenExpirationDuration),
						signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(jwtToken);

		}


		public async Task<(bool Succeeded, string Message, string Data)> ConfirmResetPasswordAsync(string email, string code)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null) return (false, "UserNotFound", "");

			var userCode = user.Code;

			if (userCode != code) return (false, "IncorrectCode", "");

			var token = await GenerateResetPasswordTokenAsync(user);

			return (true, "Success", token);

		}


		public async Task<(bool Succeeded, string Message)> ResetPasswordAsync(string token, string password)
		{
			var trans = await _appDbContext.Database.BeginTransactionAsync();
			var claimsPrincipal = _tokenHelper.ValidateToken(token);
			if (claimsPrincipal is null) return (false, "InvalideToken");
			try
			{

				var user = await _userManager.FindByEmailAsync(claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals(JwtClaimTypes.Email))?.Value ?? "");
				if (user is null) return (false, "UserNotFound");

				var valideToken = user.Jti != null
								  && user.Jti == claimsPrincipal.Claims
														.FirstOrDefault(c => c.Type.
														Equals(JwtClaimTypes.Jti))?.Value
								  && (!user.JtiExp ?? false);

				if (valideToken)
				{
					var removePassResult = await _userManager.RemovePasswordAsync(user);
					if (!removePassResult.Succeeded) return (false, "FailedRemovePassword");

					var addPassResult = await _userManager.AddPasswordAsync(user, password);

					if (!addPassResult.Succeeded) return (false, "FailedAddPassword");


					// make token expire (use one time )
					user.JtiExp = true;

					await _userManager.UpdateAsync(user);

					await trans.CommitAsync();
					return (true, "Success");

				}

				throw new InvalidDataException("Invalide token");
			}
			catch
			{
				await trans.RollbackAsync();

				return (false, "Failed");
			}
		}

		public async Task<string> ConfirmEmailByUrlAsync(int UserId, string Code)
		{
			var user = await _userManager.FindByIdAsync(UserId.ToString());

			if (user == null) return "UserNotFound";

			var result = await _userManager.ConfirmEmailAsync(user, Code);

			if (!result.Succeeded) return result.Errors?.FirstOrDefault()?.Description ?? "Failed to confirm email";

			return "Success";
		}

		public async Task<string> SendUrlConfirmEmailAsync(int userId)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(userId.ToString());


				if (user is null) return "UserNotFound";

				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

				var httpAccessor = _httpContextAccessor?.HttpContext?.Request;

				//var url = httpAccessor.Scheme + "://" + httpAccessor.Host + "/" + Router.AuthenticationRoute.ConfirmEmail + $"?userId={User.Id}&code={code}";

				var actionUrl = _urlHelper.Action("ConfirmEmail", "Authentication", new { UserId = user.Id, Code = code });

				var url = httpAccessor?.Scheme + "://" + host + actionUrl;


				var result = await _emailService.SendEmailAsync(user.Email, user.FullName, Util.FormatVerificationLink(url), "Email Confirmation from  Saidar Team");
				if (result == "Failed") throw new Exception("Cannot send Email Something wrong!");

				return "Success";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}



		}


		public async Task<string> SendCodeConfirmEmailAsync(string email, string reason = "Email Confirmation")
		{
			var trans = await _appDbContext.Database.BeginTransactionAsync();
			try
			{
				var user = await _userManager.FindByEmailAsync(email);

				if (user is null) return "UserNotFound";

				var random = new Random();

				var randomCode = random.Next(0, 100000).ToString("D6");
				user.Code = randomCode;

				var result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded) throw new DbUpdateException("ErrorUpdateUser");

				// send code to user Email

				await _emailService.SendEmailAsync(email, user.FullName, Util.FormatVerificationMessage(randomCode), reason ?? "Email Confirmation");

				await trans.CommitAsync();
				return "Success";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return ex.Message;
			}
		}

		public async Task<string> ConfirmEmailByCodeAsync(string email, string code)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null) return "UserNotFound";

			if (user.Code != code) return "IncorrectCode";

			user.EmailConfirmed = true;
			var UpdateResult = await _userManager.UpdateAsync(user);
			if (!UpdateResult.Succeeded) return "Cannot Update User";

			return "Success";
		}

		public async Task<string> SenEmailChangeAsync(string currentEmail, string newEmail)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(currentEmail);
				if (user is null) return "UserNotFound";

				var random = new Random();

				var randomCode = random.Next(0, 100000).ToString("D6");
				user.Code = randomCode;

				var result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded) throw new DbUpdateException("ErrorUpdateUser");

				// send code to user email

				await _emailService.SendEmailAsync(newEmail, user.FullName, Util.FormatVerificationMessage(randomCode), "Change email confirmation");

				return "Success";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

		}

		public async Task<string> VerifyEmailChangeAsync(string currentEmail, string newEmail, string code)
		{
			var trans = await _appDbContext.Database.BeginTransactionAsync();
			try
			{
				var user = await _userManager.FindByEmailAsync(currentEmail);
				if (user is null) return "UserNotFound";

				if (user.Code != code) return "IncorrectCode";

				user.Email = newEmail;

				var result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded) throw new DbUpdateException("Cannot Change Email");
				await trans.CommitAsync();
				return "Success";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return ex.Message;
			}

		}

		#endregion
	}
}
