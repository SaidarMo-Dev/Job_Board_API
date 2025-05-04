using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.BookMarks.Commands.Handler
{
	public class BookMarkCommandHandler : ResponseHandler,
			IRequestHandler<AddBookMarkCommand, Response<int>>,
			IRequestHandler<DeleteBookmarkByIdCommand, Response<string>>
	{

		#region Fields
		private readonly IBookmarkService _bookMarkService;
		private readonly IMapper _mapper;
		#endregion

		#region Constructors
		public BookMarkCommandHandler(IBookmarkService bookMarkService, IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_bookMarkService = bookMarkService;
			_mapper = mapper;
		}
		#endregion

		#region Handle Methods
		public async Task<Response<int>> Handle(AddBookMarkCommand request, CancellationToken cancellationToken)
		{
			var bookMark = _mapper.Map<Bookmark>(request);

			var result = await _bookMarkService.AddAsync(bookMark);

			return Created(result.BookMarkId);
		}

		public async Task<Response<string>> Handle(DeleteBookmarkByIdCommand request, CancellationToken cancellationToken)
		{
			var bookmark = await _bookMarkService.GetBookmarkByIdAsync(request.Id);
			if (bookmark is null) return NotFound<string>();

			bool IsDeleted = await _bookMarkService.DeleteByIdAsync(bookmark);

			if (!IsDeleted) return BadRequest<string>();

			return Deleted<string>();

		}


		#endregion

	}
}
