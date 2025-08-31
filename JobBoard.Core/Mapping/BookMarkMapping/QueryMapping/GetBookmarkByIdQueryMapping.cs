using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.BookMarkMapping
{
	public partial class BookMarkProfile
	{
		public void GetBookmarkByIdQueryMapping()
		{
			CreateMap<Bookmark, GetBookmarkByIdQueryResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(src => src.BookMarkId))
				.ForMember(x => x.UserID, opt => opt.MapFrom(src => src.UserId))
				.ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.userInfo.FirstName))
				.ForMember(x => x.LastName, opt => opt.MapFrom(src => src.userInfo.LastName))
				.ForMember(x => x.JobId, opt => opt.MapFrom(src => src.JobId))
				.ForMember(x => x.Title, opt => opt.MapFrom(src => src.jobListing.Title))
				.ForMember(x => x.Description, opt => opt.MapFrom(src => src.jobListing.Description))
				.ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.jobListing.Company.CompanyName))
				.ForMember(x => x.Location, opt => opt.MapFrom(src => src.jobListing.Location))
				.ForMember(x => x.JobType, opt => opt.MapFrom(src => src.jobListing.JobType.ToString()))
				.ForMember(x => x.DatePosted, opt => opt.MapFrom(src => src.jobListing.DatePosted))
				.ForMember(x => x.status, opt => opt.MapFrom(src => src.jobListing.Status.ToString()));
		}

	}
}
