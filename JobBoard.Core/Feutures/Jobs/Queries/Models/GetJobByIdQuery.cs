using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetJobByIdQuery : IRequest<Response<GetJobByIdQueryResponse>>
	{
		public int Id { get; set; }
	}
}
