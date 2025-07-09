using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using MediatR;

namespace JobBoard.Core.Feutures.ApplicationUser.Queries.Models
{
	public class GetUserDashboardStatsQuery(int Id) : IRequest<Response<GetUserDashboardStatsQueryResponse>>
	{
		public int Id { get; set; } = Id;
	}
}
