using Microsoft.AspNetCore.Authorization;

namespace JobBoard.Core.Security.Requirements
{
	public class SameUserRequirement : IAuthorizationRequirement { }
}
