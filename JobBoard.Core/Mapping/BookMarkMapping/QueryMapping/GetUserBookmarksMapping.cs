using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.BookMarkMapping
{
	public partial class BookMarkProfile
	{
		public void GetUserBookmarks()
		{
			CreateMap<Bookmark, GetUserBookmarksQueryResponse>()
				.ForMember(x => x.Job, opt => opt.MapFrom(src => src.jobListing));

			CreateMap<JobListing, JobResponseDto>()
				.ForMember(dst => dst.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ForMember(dst => dst.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName))
				.ForMember(dst => dst.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel.ToString()))
				.ForMember(dst => dst.CreatedByUser, opt => opt.MapFrom(src => src.CreatedByUser.UserName))

				.ForMember(dst => dst.Skills, opt => opt.MapFrom(src => src.JobSkills.Select(x => x.skillInfo)))
				.ForMember(dst => dst.Categories, opt => opt.MapFrom(src => src.jobCategories.Select(x => x.category)))
				;
		}
	}
}
