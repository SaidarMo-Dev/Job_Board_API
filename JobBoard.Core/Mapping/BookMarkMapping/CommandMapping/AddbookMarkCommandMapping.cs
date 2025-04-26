using JobBoard.Core.Feutures.BookMarks.Commands.Models;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.BookMarkMapping
{
	public partial class BookMarkProfile
	{
		public void AddbookMarkCommandMapping()
		{
			CreateMap<AddBookMarkCommand, Bookmark>();
		}
	}
}
