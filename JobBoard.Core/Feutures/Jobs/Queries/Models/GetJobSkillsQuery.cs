using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetJobSkillsQuery : IRequest<Response<List<GetJobSkillsQueryResponse>>>
	{
		public int JobId { get; set; }
	}
}
