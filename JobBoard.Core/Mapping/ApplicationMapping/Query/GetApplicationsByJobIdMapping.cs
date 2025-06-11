using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Data.Entities;
using JobBoard.Data.Entities.Identity;

namespace JobBoard.Core.Mapping.ApplicationMapping
{
	public partial class ApplicationProfile
	{
		public void GetApplicationsByJobIdMapping()
		{
			CreateMap<Application, ApplicationResponse>()
				.ForMember(x => x.Status, opt => opt.MapFrom(src => src.status.ToString()))
				.ForMember(x => x.Job, opt => opt.MapFrom(src => src.JobListing))
				.ForMember(x => x.User, opt => opt.MapFrom(src => src.UserInfo));


			CreateMap<JobListing, JobSummaryDto>()
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()));


			CreateMap<User, UserSummaryDto>()
			.ForMember(x => x.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
			.ForMember(x => x.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.CountryName : ""));


		}

	}
}
