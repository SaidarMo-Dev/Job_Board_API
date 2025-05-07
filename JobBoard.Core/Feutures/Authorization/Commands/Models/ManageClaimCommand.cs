using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Commands.Models
{
	public class ManageClaimsCommand : IRequest<Response<string>>
	{
	}
}
