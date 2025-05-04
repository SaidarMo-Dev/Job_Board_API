using JobBoard.Core.Bases;
using JobBoard.Data.DTOs;
using MediatR;

namespace JobBoard.Core.Feutures.Authorization.Queries.Models
{
	public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesDto>>
	{
		public int UserId { get; set; }
	}
}
