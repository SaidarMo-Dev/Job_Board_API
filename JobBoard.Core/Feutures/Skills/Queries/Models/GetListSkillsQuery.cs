using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Skills.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Queries.Models
{
	public class GetListSkillsQuery : IRequest<Response<List<GetListSkillsQueryResponse>>>
	{
	}
}
