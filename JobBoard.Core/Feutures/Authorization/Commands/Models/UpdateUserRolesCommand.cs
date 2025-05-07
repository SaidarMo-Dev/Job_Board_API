using JobBoard.Core.Bases;
using JobBoard.Data.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Commands.Models
{
	public class UpdateUserRolesCommand : ManageUserRolesDto, IRequest<Response<string>>
	{
	}
}
