using JobBoard.Core.Helpers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JobBoard.Service.Implementations
{
	public class UserService : UserManager<User>, IUserService
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly ICountryService _countryService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IEmailService _emailService;
		private readonly IUrlHelper _urlHelper;
		private readonly IUserRepository _userRepository;
		#endregion

		#region Constructors
		public UserService(IUserStore<User> userStore,
							IOptions<IdentityOptions> options,
							IPasswordHasher<User> passwordHasher,
							IEnumerable<UserValidator<User>> userValidators,
							IEnumerable<IPasswordValidator<User>> passwordValidators,
							ILookupNormalizer lookupNormalizer,
							IdentityErrorDescriber identityErrorDescriber,
							IServiceProvider serviceProvider,
							ILogger<UserManager<User>> logger,
							IUserRepository userRepository,
							UserManager<User> userManager,
							RoleManager<Role> roleManager,
							ICountryService countryService,
							IHttpContextAccessor httpContextAccessor,
							IEmailService emailService,
							IUrlHelper urlHelper
						)
						: base(userStore, options,
							passwordHasher, userValidators,
							passwordValidators, lookupNormalizer,
							identityErrorDescriber, serviceProvider,
							logger)
		{
			_userRepository = userRepository;
			_userManager = userManager;
			_roleManager = roleManager;
			_countryService = countryService;
			_httpContextAccessor = httpContextAccessor;
			_emailService = emailService;
			_urlHelper = urlHelper;
		}


		#endregion

		#region Methods
		public async Task<User> GetUserInfoByIdWithEnclude(int UserId)
		{
			return await _userManager.Users.
						Include(x => x.Country)
						.FirstOrDefaultAsync(u => u.Id.Equals(UserId));

		}

		public async Task<string> AddNewUserAsync(User User, string Password, string CountryName)
		{
			var trans = _userRepository.BeginTransaction();
			try
			{

				User.CountryId = await _countryService.GetCountryIdAsync(CountryName);

				var result = await _userManager.CreateAsync(User, Password);


				if (!result.Succeeded) return result.Errors.FirstOrDefault().Description;

				// send confirmation email
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(User);

				var httpAccessor = _httpContextAccessor.HttpContext.Request;

				//var url = httpAccessor.Scheme + "://" + httpAccessor.Host + "/" + Router.AuthenticationRoute.ConfirmEmail + $"?userId={User.Id}&code={code}";

				var actionUrl = _urlHelper.Action("ConfirmEmail", "Authentication", new { UserId = User.Id, Code = code });

				var url = httpAccessor.Scheme + "://" + httpAccessor.Host + actionUrl;




				await _emailService.SendEmail(User.Email, User.FullName, Util.FormatVerificationLink(url), "Email Confirmation from  Saidar Team");

				await trans.CommitAsync();

				return "Success";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return "Error When Creatig User: " + ex.Message;
			}
		}

		public async Task<string> UpdateUserAsync(User user)
		{

			var trans = _userRepository.BeginTransaction();
			try
			{
				await _userManager.UpdateAsync(user);
				await trans.CommitAsync();
				return "User Updated";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return "Error When Updating User: " + ex.Message;
			}
		}

		public async Task<bool> DeleteUsersAsync(User user)
		{
			var trans = _userRepository.BeginTransaction();
			try
			{
				user.IsDeleted = true;
				user.DeletedAt = DateTime.Now;

				await _userRepository.SaveChangesAsync();
				await trans.CommitAsync();
				return true;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return false;
			}
		}

		public async Task<bool> IsExistByIdAync(int UserId)
		{
			var user = await _userRepository.GetTableAsNoTracking()
				.Where(x => x.Id.Equals(UserId))
				.FirstOrDefaultAsync();

			return user != null;
		}

		public async Task<string> ConfirmEmailAsync(int UserId, string Code)
		{

			var user = await _userManager.FindByIdAsync(UserId.ToString());

			if (user == null) return "UserNotFound";

			var result = await _userManager.ConfirmEmailAsync(user, Code);

			if (!result.Succeeded) return result.Errors.FirstOrDefault().Description;

			return "Success";

		}


		#endregion
	}
}
