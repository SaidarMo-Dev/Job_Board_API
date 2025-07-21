using JobBoard.Core.Helpers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Responses;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Infrastructure.context;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
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
		private readonly appDbContext _context;
		private readonly IAuthenticationService _authenticationService;
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
							IUrlHelper urlHelper,
							appDbContext appDbContext,
							IAuthenticationService authenticationService

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
			_context = appDbContext;
			_authenticationService = authenticationService;
		}


		#endregion

		#region Methods
		public async Task<User> GetUserInfoByIdWithInclude(int UserId)
		{
			return await _userManager.Users.
						Include(x => x.Country)
						.FirstOrDefaultAsync(u => u.Id.Equals(UserId));

		}

		public async Task<string> AddNewUserAsync(User user, string password, string role)
		{
			using var trans = _userRepository.BeginTransaction();
			try
			{
				var creationResult = await _userManager.CreateAsync(user, password);
				if (!creationResult.Succeeded)
					throw new Exception(creationResult.Errors.FirstOrDefault()?.Description);

				user.Code = Util.GenerateSixDigitCode();

				var roleAdded = await AddUserToRoleAsync(user, role);
				if (!roleAdded)
					throw new Exception("Failed to assign role to user.");

				var emailResult = await _emailService.SendEmail(
					user.Email!,
					user.FullName,
					Util.FormatVerificationMessage(user.Code),
					"Email Confirmation");


				if (emailResult != "Success")
					throw new Exception("Failed to send verification email.");

				await trans.CommitAsync();
				return "Success";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				Log.Error(ex, "Failed to add user");
				return "An error occurred while creating the user.";
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

		public async Task<bool> IsEmailExistAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			return user is not null;
		}

		public async Task<DashboardStatsResponse> GetUserDashboardStatsAsync(int userId)
		{
			var result = await _context.Users.Where(x => x.Id == userId).Select(u => new DashboardStatsResponse
			{
				TotalSavedJobs = u.bookmarks.Count(),
				TotalApplications = u.applications.Count(),
				Rejected = u.applications.Where(x => x.status == Data.enums.ApplicationStatusEnum.Rejected).Count(),
				Pending = u.applications.Where(x => x.status == Data.enums.ApplicationStatusEnum.Pending).Count(),
			}).FirstOrDefaultAsync();

			return result;
		}


		#endregion
	}
}
