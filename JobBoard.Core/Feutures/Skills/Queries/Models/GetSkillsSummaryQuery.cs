using JobBoard.Core.Feutures.Skills.Queries.Results;
using JobBoard.Core.Wrapers;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Queries.Models
{
	public class GetSkillsSummaryQuery : IRequest<PaginatedResponse<List<GetSkillsSummaryQueryResponse>>>
	{
		public int page { get; set; }
		public int Size { get; set; }
	}
}
