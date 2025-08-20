using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Companies.Queries.Models
{
	public class GetPopularCompaniesQuery : IRequest<Response<string[]>>
	{
	}
}
