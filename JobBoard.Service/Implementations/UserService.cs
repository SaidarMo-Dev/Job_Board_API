using JobBoard.Data.Entities.Identity;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JobBoard.Service.Implementations
{
	public class UserService : UserManager<User>, IUserService
	{
		#region Fields
		private readonly UserManager<User> _userManager;
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
							UserManager<User> userManager

						)
						: base(userStore, options,
							passwordHasher, userValidators,
							passwordValidators, lookupNormalizer,
							identityErrorDescriber, serviceProvider,
							logger)
		{
			_userRepository = userRepository;
			_userManager = userManager; ;
		}


		#endregion

		#region Methods
		public async Task<User> GetUserInfoByIdWithEnclude(int UserId)
		{
			return await _userManager.Users.
						Include(x => x.Country)
						.FirstOrDefaultAsync(u => u.Id.Equals(UserId));

		}

		public async Task<string> AddNewUser(User User, string Password)
		{
			var trans = _userRepository.BeginTransaction();
			try
			{
				await _userManager.CreateAsync(User, Password);
				await trans.CommitAsync();
				return "User Created";
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return "Error When Creatig User: " + ex.Message;
			}
		}

		public async Task<string> UpdateUser(User user)
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
		#endregion
	}
}
