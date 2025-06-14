using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

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
		public async Task<User> GetUserInfoByIdWithInclude(int UserId)
		{
			return await _userManager.Users.
						Include(x => x.Country)
						.FirstOrDefaultAsync(u => u.Id.Equals(UserId));

		}

		public async Task<string> AddNewUserAsync(User User, string Password, string role)
		{
			var trans = _userRepository.BeginTransaction();
			try
			{

				var result = await _userManager.CreateAsync(User, Password);


				if (!result.Succeeded) throw new Exception(result.Errors?.FirstOrDefault()?.Description);

				var addedRole = await AddUserToRoleAsync(User, role);
				if (!addedRole) throw new Exception("Can't Add role to user");

				await trans.CommitAsync();

				return "Success";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();

				Log.Error(ex, "Error: " + ex.Message);

				return "Error:  " + ex.Message;
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
				Log.Error(ex, "Error When Updating User: " + ex.Message);

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
				Log.Error(ex, "Error When Delete User : " + ex.Message);
				return false;
			}
		}

		public async Task<bool> IsExistByIdAsync(int UserId)
		{
			var user = await _userRepository.GetTableAsNoTracking()
				.Where(x => x.Id.Equals(UserId))
				.FirstOrDefaultAsync();

			return user != null;
		}

		private async Task<bool> AddUserToRoleAsync(User user, string role)
		{
			var exist = await _roleManager.RoleExistsAsync(role);
			if (!exist)
			{
				await _roleManager.CreateAsync(new Role { Name = role, ConcurrencyStamp = null, NormalizedName = role.ToUpper() });
			}

			var result = await _userManager.AddToRoleAsync(user, role);

			return result.Succeeded;
		}


		#endregion
	}
}
