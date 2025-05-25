using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void GetCurrentUserApplicationsMapping()
		{
			CreateMap<Application, UserApplicationResponse>()
				.ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.status.ToString()))
				.ForMember(dst => dst.Job, opt => opt.MapFrom(src => src.JobListing));


			//CreateMap<JobListing, JobSummaryDto>()
			//	.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
			//	.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()));

		}
	}
}
