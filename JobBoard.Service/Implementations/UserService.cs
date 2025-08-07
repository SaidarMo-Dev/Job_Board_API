using JobBoard.Core.Helpers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;
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
		private readonly appDbContext appDbContext1;

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

		public IQueryable<UserManagementResponse> GetUsersQueryable(string? search, FilterByRole? role, FilterByStatus? status)
		{
			var users = _userManager.Users.IgnoreQueryFilters().AsNoTracking();

			// Handle search by email or full name

			if (search != null)
			{
				// if search contains @ then search by email
				// else we treat search as full name 

				if (search.Contains("@"))
				{
					users = users.Where(x => x.Email != null && x.Email.Contains(search));
				}
				else
				{
					var names = search.Split(" ", StringSplitOptions.RemoveEmptyEntries);

					if (names.Length > 1)
					{
						users = users.Where(x => x.FirstName.Contains(names[0]) && x.LastName.Contains(names[1]));
					}
					else users = users.Where(x => x.FirstName.Contains(names[0]));

				}


			}

			// filter by status

			if (status != null && status != FilterByStatus.All)
			{
				users = users.Where(x => status == FilterByStatus.Suspended ? x.IsDeleted : !x.IsDeleted);
			}



			// filter by role
			if (role != null && role != FilterByRole.All)
			{
				var roleInfo = _roleManager.Roles.FirstOrDefault(x => x.Name == role.ToString());
				var roleId = roleInfo is null ? -1 : roleInfo.Id;

				users = users.Join(_context.userRoles.Where(r => r.RoleId.Equals(roleId)), u => u.Id, r => r.UserId, (user, role) => user);


			}

			var query = from user in users
						join country in _context.countries on user.CountryId equals country.CountryId into countryJoin
						from country in countryJoin.DefaultIfEmpty()

						from userRole in _context.UserRoles.Where(ur => ur.UserId == user.Id).DefaultIfEmpty()
						from r in _context.Roles.Where(r => r.Id == userRole.RoleId).DefaultIfEmpty()

						select new UserManagementResponse
						{
							Id = user.Id,
							FirstName = user.FirstName,
							LastName = user.LastName,
							Email = user.Email,
							Username = user.UserName,
							PhoneNumber = user.PhoneNumber,
							Address = user.Address,
							Gender = user.Gender.ToString(),
							DateOfBirth = user.DateOfBirth,
							ImagePath = user.ImagePath,
							IsDeleted = user.IsDeleted,
							DeletedAt = user.DeletedAt,
							country = country != null ? country.CountryName : "Unknown",
							Role = r.Name ?? "Unknown"
						};

			return query;
		}

		public async Task<IdentityResult> AdminUpdateUserAsync(User user, string role)
		{
			using var trans = _userRepository.BeginTransaction();

			try
			{
				// Update the user
				var updateUserResult = await _userManager.UpdateAsync(user);
				if (!updateUserResult.Succeeded)
					throw new DbUpdateException(updateUserResult.Errors.FirstOrDefault()?.Description ?? "Cannot update user");

				// Remove old role
				var oldRoles = await _userManager.GetRolesAsync(user);
				if (oldRoles.Count > 0)
				{
					var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, oldRoles);
					if (!removeRoleResult.Succeeded)
						throw new DbUpdateException(removeRoleResult.Errors.FirstOrDefault()?.Description ?? "Cannot remove old roles");
				}

				// Add new role
				var addRoleResult = await _userManager.AddToRoleAsync(user, role);
				if (!addRoleResult.Succeeded)
					throw new DbUpdateException(addRoleResult.Errors.FirstOrDefault()?.Description ?? "Cannot add new role");

				await trans.CommitAsync();
				return IdentityResult.Success;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();

				Log.Error(ex, "Failed to update user");

				return IdentityResult.Failed(new IdentityError
				{
					Code = "UpdateFailed",
					Description = ex.Message
				});
			}
		}

		public async Task<User> GetAdminProfile(int userId)
		{
			var user = await _userRepository.GetTableAsNoTracking().Where(x => x.Id == userId).Include(x => x.Country).FirstOrDefaultAsync();
			return user;
		}

		#endregion
	}

}