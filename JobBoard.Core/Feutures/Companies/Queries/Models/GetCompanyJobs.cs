using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetCompanyJobs : IRequest<PaginatedResponse<GlobalJobResponseDto>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public required string Slug { get; set; }
	}
}
