using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Skills.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Skills.Queries.Models
{
	public class GetSingleSkillQuery : IRequest<Response<GetSingleSkillQueryResponse>>
	{
		public int Id { get; set; }
		public GetSingleSkillQuery(int id)
		{
			Id = id;
		}
	}
}
