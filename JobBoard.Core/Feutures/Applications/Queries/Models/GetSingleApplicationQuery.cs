using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Queries.Models
{
	public class GetSingleApplicationQuery : IRequest<Response<GetSingleApplictionQueryResponse>>
	{
		public int Id { get; set; }
	}
}
