using JobBoard.Core.Feutures.Employer.Queries.Responses;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Employer.Queries.Models
{
	public class GetEmployerPostedJobsQuery : IRequest<PaginatedResponse<GetEmployerPostedJobsQueryResponse>>
	{
		public int Page { get; set; }
		public int Size { get; set; }
		public string? Search { get; set; }
	}
}
