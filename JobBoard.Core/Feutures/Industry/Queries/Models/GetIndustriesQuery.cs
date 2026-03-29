using JobBoard.Core.Feutures.Industry.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Industry.Queries.Models
{
	public class GetIndustriesQuery : IRequest<PaginatedResponse<GetIndustriesQueryResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
	}
}
