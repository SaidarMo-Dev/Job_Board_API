using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Models
{
	public class GetPaginatedListUsersQuery : IRequest<PaginatedResponse<List<GetPaginatedListUsersQueryResponse>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

	}
}
