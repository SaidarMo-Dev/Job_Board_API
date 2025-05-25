using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Applications.Queries.Models
{
	public class GetApplicationsByJobIdQuery : IRequest<Response<GetApplicationsByJobIdQueryResponse>>
	{
		public int JobId { get; set; }
		public GetApplicationsByJobIdQuery(int jobId)
		{
			JobId = jobId;
		}
	}
}
