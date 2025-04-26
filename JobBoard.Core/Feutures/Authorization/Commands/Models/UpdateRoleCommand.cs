using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Commands.Models
{
	public class UpdateRoleCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public required string RoleName { get; set; }
	}
}
