using JobBoard.Core.Feutures.ApplicationUser.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.UserMapping
{
	public partial class UserProfile
	{
		public void GetUseBookmarksMapping()
		{
			CreateMap<Bookmark, GetUseBookmarksQueryResponse>()
				.ForMember(x => x.UserId, opt => opt.MapFrom(src => src.UserId))
				.ForMember(x => x.Username, opt => opt.MapFrom(src => src.userInfo.UserName))
				.ForMember(x => x.BookmarkResponse, opt => opt.MapFrom(src => src));

			CreateMap<Bookmark, BookmarkResponse>()
				.ForMember(x => x.BookmarkId, opt => opt.MapFrom(src => src.BookMarkId))
				.ForMember(x => x.DateBooked, opt => opt.MapFrom(src => src.DateBooked))
				.ForMember(x => x.JobId, opt => opt.MapFrom(src => src.jobListing.JobId))
				.ForMember(x => x.Title, opt => opt.MapFrom(src => src.jobListing.Title))
				.ForMember(x => x.Description, opt => opt.MapFrom(src => src.jobListing.Description))
				.ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.jobListing.company.CompanyName))
				.ForMember(x => x.Location, opt => opt.MapFrom(src => src.jobListing.Location))
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.jobListing.JobType.ToString()))
				.ForMember(x => x.SalaryRange, opt => opt.MapFrom(src => src.jobListing.SalaryRange))
				.ForMember(x => x.DatePosted, opt => opt.MapFrom(src => src.jobListing.DatePosted))
				.ForMember(x => x.status, opt => opt.MapFrom(src => src.jobListing.Status.ToString()));


		}

	}
}
