using AutoMapper;

namespace JobBoard.Core.Mapping.BookMarkMapping
{
	public partial class BookMarkProfile : Profile
	{
		public BookMarkProfile()
		{
			AddbookMarkCommandMapping();
			GetBookmarkByIdQueryMapping();
			GetPaginatedBookmarkListMapping();
			GetUserBookmarks();

		}
	}
}
