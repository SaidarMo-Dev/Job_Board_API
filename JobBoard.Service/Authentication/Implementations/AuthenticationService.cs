using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

namespace JobBoard.Service.Authentication.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		#region Fields
		private readonly JwtSettings _jwtSettings;
		private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly appDbContext _appDbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string host = "http://localhost:5173";
		private readonly IUrlHelper _urlHelper;
		#endregion

		#region Constructors
		public AuthenticationService(JwtSettings jwtSettings,
									 IUserRefreshTokenRepository userRefreshTokenRepository,
									 UserManager<User> userManager,
									 IEmailService emailService,
									 appDbContext appDbContext,
									 IHttpContextAccessor httpContextAccessor,
									 IUrlHelper urlHelper
									)
		{
			_jwtSettings = jwtSettings;
			_userRefreshTokenRepository = userRefreshTokenRepository;
			_userManager = userManager;
			_emailService = emailService;
			_appDbContext = appDbContext;
			_httpContextAccessor = httpContextAccessor;
			_urlHelper = urlHelper;
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
		private async Task<List<Claim>> _GetUserClaimsAsync(User user, List<string> roles)
		{

			var userClaims = await _userManager.GetClaimsAsync(user);

			var claims = new List<Claim>()
			{
				new Claim(nameof(JwtClaimModel.UserId), user?.Id.ToString()),
				new Claim(ClaimTypes.Name.ToString(), user.UserName),
				new Claim(ClaimTypes.Email.ToString(), user.Email),
				new Claim(nameof(JwtClaimModel.FirstName), user.FirstName),
				new Claim(nameof(JwtClaimModel.LastName), user.LastName)
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
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
				Username = user.UserName,
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

			// return response of access and refresh tokens 
			return new AuthResponse
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken
			};

		}

		public async Task<AuthResponse> GetRefreshToken(string refreshToken, string accessToken)
		{
			var jwtToken = ReadJwtToken(accessToken);

			if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
				throw new SecurityTokenException("Invalid Token Info");

			if (jwtToken.ValidTo > DateTime.UtcNow)
				throw new SecurityTokenException("Token Not Expired");

			var userId = jwtToken.Claims.Where(x => x.Type.Equals(nameof(JwtClaimModel.UserId)))
							.FirstOrDefault()?.Value;

			if (string.IsNullOrEmpty(userId))
				throw new ArgumentNullException(nameof(userId));

			var userrefreshToken = await _userRefreshTokenRepository.GetTableAsTracking()
								.FirstOrDefaultAsync(x => x.RefreshToken.Equals(refreshToken) &&
													x.AccessToken.Equals(accessToken) &&
													x.UserId == int.Parse(userId));

			if (userrefreshToken is null)
				throw new SecurityTokenException("Invalid Token Info");

			if (!userrefreshToken.IsActive)
				throw new SecurityTokenException("Refresh token Is Expired");

			var user = await _userManager.FindByIdAsync(userId);
			if (user is null)
				throw new ArgumentNullException("User Not Found");

			var newAccessToken = await _GenerateAccessTokenAsync(user);

			// Update Access token
			userrefreshToken.AccessToken = newAccessToken;
			await _userRefreshTokenRepository.UpdateAsync(userrefreshToken);

			// return AuthResponse 
			return new AuthResponse
			{
				AccessToken = newAccessToken,
				RefreshToken = new RefreshTokenResponse
				{
					Username = user.UserName,
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

		public async Task<string> SendResetPasswordAsync(string Email)
		{
			var trans = await _appDbContext.Database.BeginTransactionAsync();
			try
			{
				var user = await _userManager.FindByEmailAsync(Email);

				if (user is null) return "UserNotFound";

				var random = new Random();

				var randomCode = random.Next(0, 100000).ToString("D6");
				user.Code = randomCode;

				var result = await _userManager.UpdateAsync(user);
				if (!result.Succeeded) return "ErrorUpdateUser";

				// send code to user Email

				await _emailService.SendEmail(Email, user.FullName, Util.FormatVerificationMessage(randomCode), "Your Verification Code");

				await trans.CommitAsync();
				return "Success";
			}
			catch
			{
				await trans.RollbackAsync();
				return "Failed";
			}

		}

		public async Task<string> ConfirmResetPasswordAsync(string email, string code)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null) return "UserNotFound";

			var userCode = user.Code;

			if (userCode != code) return "IncorrectCode";

			return "Success";

		}

		public async Task<string> ResetPasswordAsync(string email, string password)
		{
			var trans = await _appDbContext.Database.BeginTransactionAsync();

			try
			{
				var user = await _userManager.FindByEmailAsync(email);
				if (user is null) return "UserNotFound";

				var removePassResult = await _userManager.RemovePasswordAsync(user);
				if (!removePassResult.Succeeded) return "FailedRemovePassword";

				var addPassResult = await _userManager.AddPasswordAsync(user, password);

				if (!addPassResult.Succeeded) return "FailedAddPassword";

				await trans.CommitAsync();
				return "Success";


			}
			catch
			{
				await trans.RollbackAsync();
				return "Failed";
			}
		}

		public async Task<string> ConfirmEmailAsync(int UserId, string Code)
		{
			var user = await _userManager.FindByIdAsync(UserId.ToString());

			if (user == null) return "UserNotFound";

			var result = await _userManager.ConfirmEmailAsync(user, Code);

			if (!result.Succeeded) return result.Errors?.FirstOrDefault()?.Description;

			return "Success";
		}

		public async Task<string> SendConfirmEmailAsync(int userId)
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


				var result = await _emailService.SendEmail(user.Email, user.FullName, Util.FormatVerificationLink(url), "Email Confirmation from  Saidar Team");
				if (result == "Failed") throw new Exception("Cannot send Email Something wrong!");

				return "Success";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}



		}

		#endregion
	}
}
