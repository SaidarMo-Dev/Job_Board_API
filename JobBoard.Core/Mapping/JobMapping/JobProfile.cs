using AutoMapper;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile : Profile
	{
		public JobProfile()
		{
			AddJobCommandMapping();
			GetJobByIdQueryMapping();
			GetPaginatedJobsQueryMapping();
			GetJobSkillsMapping();
			UpdateJobCommandMapping();
			GetJobsByCompanyIdMapping();
			MapGetJobByIdSummaryQuery();
		}
	}
}
