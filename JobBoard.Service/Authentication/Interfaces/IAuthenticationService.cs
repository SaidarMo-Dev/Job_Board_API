using System.IdentityModel.Tokens.Jwt;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;

namespace JobBoard.Service.Authentication.Interfaces
{
	public interface IAuthenticationService
	{
		Task<AuthResponse> GenerateUserToken(User user);
		JwtSecurityToken ReadJwtToken(string accessToken);
		Task<AuthResponse> GetRefreshToken(string refreshToken, string accessToken);
		Task<string> SendResetPasswordAsync(string email);
		Task<string> ConfirmResetPasswordAsync(string email, string code);
		Task<string> ResetPasswordAsync(string email, string password);

		Task<string> SendUrlConfirmEmailAsync(int userId);
		/// <summary>
		/// Confirms a user's email asynchronously using the provided confirmation code.
		/// </summary>
		/// <param name="UserId">The ID of the user.</param>
		/// <param name="Code">The confirmation code.</param>
		/// <returns>A string indicating the result of the email confirmation.</returns>
		Task<string> ConfirmEmailByUrlAsync(int UserId, string Code);

		Task<string> SendCodeConfirmEmailAsync(string email);
		Task<string> ConfirmEmailByCodeAsync(string email, string code);
		Task<string> SenEmailChangeAsync(string currentEmail, string newEmail);
		Task<string> VerifyEmailChangeAsync(string currentEmail, string newEmail, string code);


	}
}
