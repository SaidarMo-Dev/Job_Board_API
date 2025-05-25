using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.BookMarkMapping
{
	public partial class BookMarkProfile
	{
		public void GetUserBookmarks()
		{
			CreateMap<Bookmark, BookmarkResponse>()
				.ForMember(x => x.Job, opt => opt.MapFrom(src => src.jobListing));

			CreateMap<JobListing, BookmarkJobResponse>()
				.ForMember(dst => dst.JobType, opt => opt.MapFrom(src => src.JobType.ToString()))
				.ForMember(dst => dst.status, opt => opt.MapFrom(src => src.Status.ToString()))
				.ForMember(dst => dst.CompanyName, opt => opt.MapFrom(src => src.company.CompanyName));
		}
	}
}
