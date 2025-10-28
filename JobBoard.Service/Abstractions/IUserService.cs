using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;
using JobBoard.Data.Responses;
using Microsoft.AspNetCore.Identity;

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


		/// <summary>
		/// Retrieves user dashboard stats asynchronously by user ID.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>The <see cref="DashboardStatsResponse"/> entity with related data if found; otherwise, null.</returns>
		Task<DashboardStatsResponse> GetUserDashboardStatsAsync(int userId);

		/// <summary>
		/// Retrieves users queryable.
		/// </summary>
		/// <param name="search">Search users by name.</param>
		/// <param name="role">filter users by role.</param>
		/// <param name="status">filter users by status.</param>
		/// <returns>A queryable collection of the <see cref="UserManagementResponse"/> entity with related data if found; otherwise, null.</returns>
		IQueryable<UserManagementResponse> GetUsersQueryable(string? search, FilterByRole? role, FilterByStatus? status);

		/// <summary>
		/// Updates an existing user asynchronously and assign a new role if exist.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity to update.</param>
		/// <param name="role">The new role to update.</param>
		/// <returns>An <see cref="IdentityResult"/>  indicating the result of the update operation.</returns>
		Task<IdentityResult> AdminUpdateUserAsync(User user, string role);

		/// <summary>
		/// Retrieves Admin profile.
		/// </summary>
		/// <param name="userId">The ID of the Admin.</param>
		/// <returns>The <see cref="User"/> entity with related data if found; otherwise, null.</returns>
		Task<User> GetAdminProfile(int userId);

		/// <summary>
		/// Retrieves Employer dashboard stats asynchronously by Employer ID.
		/// </summary>
		/// <param name="userId">The ID of the Employer.</param>
		/// <returns>The <see cref="EmployerDashboardStats"/> entity with related data if found; otherwise, null.</returns>
		Task<EmployerDashboardStats> GetEmployerDashboardStats(int userId);

	}
}
