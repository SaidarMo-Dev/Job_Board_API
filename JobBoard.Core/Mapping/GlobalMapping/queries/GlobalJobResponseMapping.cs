using JobBoard.Core.Common.DTOs;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.GlobalMapping
{
	public partial class GlobaleMappingProfile
	{
		public void MapGlobalJobResponse()
		{
			CreateMap<JobListing, GlobalJobResponseDto>()
			.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
			.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()))
			.ForMember(x => x.DatePosted, opt => opt.MapFrom(src => src.DatePosted))
			.ForMember(x => x.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel.ToString()))
			.ForMember(x => x.Company, opt => opt.MapFrom(src => src.Company))
			.ForMember(x => x.Skills, opt => opt.MapFrom(src => src.JobSkills.Select(x => x.skillInfo)))
			.ForMember(x => x.Categories, opt => opt.MapFrom(src => src.jobCategories.Select(x => x.category)))
			.ForMember(x => x.CretaedByUser, opt => opt.MapFrom(src => src.CreatedByUser.FullName));


		}
	}
}
