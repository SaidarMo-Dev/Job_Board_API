using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

		public async Task<bool> DeleteBookmarkAsync(Bookmark bookmark)
		{
			try
			{
				await _bookMarkRepository.DeleteAsync(bookmark);
				return true;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error :" + ex.Message);
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
							.Include(x => x.jobListing).ThenInclude(x => x.company)
							.Where(x => x.UserId.Equals(UserId))
							.ToListAsync();

		}

		public IQueryable<Bookmark> GetUserBookmarksQueryable(int userId)
		{
			return _bookMarkRepository.GetTableAsNoTracking().Where(x => x.UserId.Equals(userId)).AsQueryable();
		}

		public Task<int> GetUserSavedJobsCount(int userId)
		{
			return _bookMarkRepository.GetTableAsNoTracking().Where(x => x.UserId.Equals(userId)).CountAsync();
		}
		public async Task<List<int>> GetUserSavedJobIds(int userId)
		{
			var result = await _bookMarkRepository.GetTableAsNoTracking()
								.Where(x => x.UserId.Equals(userId))
								.Select(x => x.JobId).ToListAsync();

			return result;
		}

		public async Task<Bookmark> GetUserBookmarkAsync(int userId, int JobId)
		{
			var result = await _bookMarkRepository.GetTableAsNoTracking()
				.FirstOrDefaultAsync(x => x.JobId.Equals(JobId) && x.UserId.Equals(userId));

			return result;
		}


		#endregion
	}
}