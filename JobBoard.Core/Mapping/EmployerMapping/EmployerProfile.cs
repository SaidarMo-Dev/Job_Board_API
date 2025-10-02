using AutoMapper;

namespace JobBoard.Core.Mapping.EmployerMapping
{
	public partial class EmployerProfile : Profile
	{
		public EmployerProfile()
		{
			MapGetEmployerDashboardStats();
			MapGetEmployerPostedJobs();
		}
	}
}
