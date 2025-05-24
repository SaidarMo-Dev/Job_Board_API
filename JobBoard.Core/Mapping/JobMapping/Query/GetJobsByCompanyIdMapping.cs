using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void GetJobsByCompanyIdMapping()
		{
			CreateMap<JobListing, JobResponse>()
				.ForMember(dst => dst.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.status.ToString()));

		}
	}
}
