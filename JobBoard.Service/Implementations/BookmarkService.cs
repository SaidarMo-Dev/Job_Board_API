using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class BookmarkService : IBookmarkService
	{
		#region Fields

		private readonly IBookMarkRepository _bookMarkRepository;
		#endregion

		#region Constructors
		public BookmarkService(IBookMarkRepository bookMarkRepository)
		{
			_bookMarkRepository = bookMarkRepository;

		}


		#endregion

		#region Methods
		public Task<Bookmark> AddAsync(Bookmark bookMark)
		{
			var result = _bookMarkRepository.AddAsync(bookMark);
			return result;
		}

		public async Task<bool> DeleteByIdAsync(Bookmark bookmark)
		{
			try
			{
				await _bookMarkRepository.DeleteAsync(bookmark);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Bookmark> GetBookmarkByIdAsync(int Id)
		{
			var result = await _bookMarkRepository.GetTableAsNoTracking()
				.Where(x => x.BookMarkId.Equals(Id))
				.FirstOrDefaultAsync();

			return result;
		}

		public async Task<Bookmark> GetBookmarkByIdWithIncludeAsync(int Id)
		{
			var result = await _bookMarkRepository.GetTableAsNoTracking()
				.Include(x => x.userInfo)
				.Include(x => x.jobListing).ThenInclude(x => x.company)
				.Where(x => x.BookMarkId.Equals(Id))
				.FirstOrDefaultAsync();

			return result;
		}

		public IQueryable<Bookmark> GetBookmarksQueryable()
		{
			return _bookMarkRepository.GetTableAsNoTracking().AsQueryable();
		}

		public async Task<List<Bookmark>> GetUserBookmarks(int UserId)
		{
			return await _bookMarkRepository.GetTableAsNoTracking()
							.Include(x => x.userInfo)
							.Include(x => x.jobListing).ThenInclude(x => x.company)
							.Where(x => x.UserId.Equals(UserId))
							.ToListAsync();

		}

		#endregion
	}
}