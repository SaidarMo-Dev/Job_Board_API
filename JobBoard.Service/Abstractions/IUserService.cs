using JobBoard.Data.Entities.Identity;

namespace JobBoard.Service.Abstractions
{
	public interface IUserService
	{
		/// <summary>
		/// Retrieves user information by user ID including related entities.
		/// </summary>
		/// <param name="UserId">The ID of the user.</param>
		/// <returns>The <see cref="User"/> entity with related data if found; otherwise, null.</returns>
		Task<User> GetUserInfoByIdWithInclude(int UserId);

		/// <summary>
		/// Adds a new user asynchronously with the specified password and country name.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity to add.</param>
		/// <param name="Password">The password for the new user.</param>
		/// <param name="role">The role for the new user.</param>

		/// <returns>A string indicating the result of the add operation.</returns>
		Task<string> AddNewUserAsync(User user, string Password, string role);

		/// <summary>
		/// Updates an existing user asynchronously.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity to update.</param>
		/// <returns>A string indicating the result of the update operation.</returns>
		Task<string> UpdateUserAsync(User user);

		/// <summary>
		/// Deletes a user asynchronously.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity to delete.</param>
		/// <returns><c>true</c> if the user was deleted successfully; otherwise, <c>false</c>.</returns>
		Task<bool> DeleteUsersAsync(User user);

		/// <summary>
		/// Checks asynchronously if a user exists by ID.
		/// </summary>
		/// <param name="UserId">The ID of the user.</param>
		/// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByIdAsync(int UserId);

		/// <summary>
		/// Checks asynchronously if the email exists.
		/// </summary>
		/// <param name="email">The email we want to check.</param>
		/// <returns><c>true</c> if the email exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsEmailExistAsync(string email);

	}
}
