using System.IdentityModel.Tokens.Jwt;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;

namespace JobBoard.Service.Authentication
{
	public interface IAuthenticationService
	{
		Task<AuthResponse> GenerateUserToken(User user);
		JwtSecurityToken ReadJwtToken(string accessToken);
		Task<AuthResponse> GetRefreshToken(string refreshToken, string accessToken);

	}
}
