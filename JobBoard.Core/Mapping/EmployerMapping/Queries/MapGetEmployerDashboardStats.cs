using JobBoard.Core.Feutures.Employer.Queries.Responses;
using JobBoard.Data.Responses;

namespace JobBoard.Core.Mapping.EmployerMapping
{
	public partial class EmployerProfile
	{
		public void MapGetEmployerDashboardStats()
		{
			CreateMap<EmployerDashboardStats, GetEmployerDashboardStatsQueryResponse>();
		}
	}
}
