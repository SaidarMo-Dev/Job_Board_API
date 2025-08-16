using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.Admin.Query.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.AdminMapping
{
	public partial class AdminProfile
	{
		public void GetAdminJobsQueryMapping()
		{
			CreateMap<JobListing, GetAdminJobsQueryResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.JobId))
				.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(x => x.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel.ToString()))
				.ForMember(x => x.ApplicantsCount, opt => opt.MapFrom(src => src.applications.Count()))
				.ForMember(x => x.createdBy, opt => opt.MapFrom(src => src.CreatedByUser.Email))
				.ForMember(x => x.Skills, opt => opt.MapFrom(src => src.JobSkills.Select(x => x.skillInfo)))
				.ForMember(x => x.Categories, opt => opt.MapFrom(src => src.jobCategories.Select(x => x.category)));

			CreateMap<Company, CompanyDto>();

		}
	}
}
