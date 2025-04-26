using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Commands.Models
{
	public class DeleteRoleCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
	}
}
