using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.JobMapping
{
	public partial class JobProfile
	{
		public void MapGetPaginatedJobsQuery()
		{
			CreateMap<JobListing, GetPaginatedJobsQueryResponse>()
				.ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ForMember(x => x.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel.ToString()))
				.ForMember(x => x.CretaedByUser, opt => opt.MapFrom(src => src.CreatedByUser.FullName))

				.ForMember(x => x.Skills, opt => opt.MapFrom(src => src.JobSkills.Select(x => x.skillInfo)))
				.ForMember(x => x.Categories, opt => opt.MapFrom(src => src.jobCategories.Select(x => x.category)));


			CreateMap<Company, CompanyPreviewDto>()
			.ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));




		}
	}
}
