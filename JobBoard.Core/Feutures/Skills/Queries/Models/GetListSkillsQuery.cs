using JobBoard.Core.Feutures.Skills.Queries.Results;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Queries.Models
{
	public class GetListSkillsQuery : IRequest<PaginatedResponse<GetListSkillsQueryResponse>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public SortSkill? SortBy { get; set; }
	}

}
