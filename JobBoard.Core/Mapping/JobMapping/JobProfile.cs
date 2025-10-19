using AutoMapper;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile : Profile
	{
		public JobProfile()
		{
			MapAddJobCommand();
			MapGetJobByIdQuery();
			MapGetPaginatedJobsQuery();
			MapGetJobSkills();
			MapUpdateJobCommand();
			MapGetJobsByCompanyId();
			MapGetJobByIdSummaryQuery();
			MapGetJobApplicantsSummary();
		}
	}
}
