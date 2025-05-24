using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetJobsByCompanyIdQuery : IRequest<Response<GetJobsByCompanyIdQueryResponse>>
	{
		public int CompanyId { get; set; }

		public GetJobsByCompanyIdQuery(int Id)
		{
			CompanyId = Id;
		}
	}
}
