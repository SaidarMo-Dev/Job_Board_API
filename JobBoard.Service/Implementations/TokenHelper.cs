using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JobBoard.Service.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobBoard.Service.Implementations
{
	public class TokenHelper : ITokenHelper
	{
		private readonly TokenValidationParameters _tokenValidationParameters;

		public TokenHelper(IOptionsMonitor<JwtBearerOptions> jwtOptions)
		{
			_tokenValidationParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
		}

		public ClaimsPrincipal? ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				return tokenHandler.ValidateToken(token, _tokenValidationParameters, out _);

			}
			catch
			{
				return null;
			}
		}
	}
}
