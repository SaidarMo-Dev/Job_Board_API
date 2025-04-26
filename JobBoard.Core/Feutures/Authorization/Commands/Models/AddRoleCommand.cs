using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Commands.Models
{
	public class AddRoleCommand : IRequest<Response<int>>
	{
		public required string RoleName { get; set; }
	}
}
