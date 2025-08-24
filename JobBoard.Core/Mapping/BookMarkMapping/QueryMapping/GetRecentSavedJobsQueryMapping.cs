using JobBoard.Core.Feutures.BookMarks.Queries.Responses;
using JobBoard.Data.Responses;

namespace JobBoard.Core.Mapping.BookMarkMapping
{
	public partial class BookMarkProfile
	{
		public void GetRecentSavedJobsQueryMapping()
		{
			CreateMap<RecentSavedJobsResponse, GetRecentSavedJobsQueryResponse>()
				.ForMember(x => x.SavedAt, opt =>
						opt.MapFrom(src =>
						new DateOnly(src.SavedAt.Year, src.SavedAt.Month, src.SavedAt.Day)));
		}
	}
}
