using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Employer.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.Employer.Queries.Models
{
	public class GetEmployerDashboardStatsQuery : IRequest<Response<GetEmployerDashboardStatsQueryResponse>>
	{
	}
}
