using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void GetCurrentUserApplicationsMapping()
		{
			CreateMap<Application, GetCurrentUserApplicationsQueryResponse>()
				.ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.status.ToString()))
				.ForMember(dst => dst.Job, opt => opt.MapFrom(src => src.JobListing));

			CreateMap<JobListing, JobSummaryDto>()
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.company.CompanyName))
				.ForMember(x => x.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel.ToString()))
				.ForMember(x => x.Skills, opt => opt.MapFrom(src => src.JobSkills.Select(x => x.skillInfo)))
				.ForMember(x => x.Categories, opt => opt.MapFrom(src => src.jobCategories.Select(x => x.category)));

		}
	}
}
