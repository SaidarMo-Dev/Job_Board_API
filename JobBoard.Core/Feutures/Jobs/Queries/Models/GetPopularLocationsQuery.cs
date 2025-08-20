using JobBoard.Core.Bases;
using MediatR;

namespace JobBoard.Core.Feutures.Jobs.Queries.Models
{
	public class GetPopularLocationsQuery : IRequest<Response<string[]>>
	{
	}
}
