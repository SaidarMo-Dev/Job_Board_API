using JobBoard.Core.Bases;
using JobBoard.Data.Requests;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Commands.Models
{
	public class UpdateUserClaimCommand : UpdateUserClaimRequest, IRequest<Response<string>>
	{
	}
}
