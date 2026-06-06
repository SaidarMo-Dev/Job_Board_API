using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Companies.Queries.Results;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetCompanyStatisticsQuery : IRequest<Response<GetStatisticsQueryResponse>>
	{
	}
}
