using AutoMapper;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.BookMarks.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.BookMarks.Commands.Handler
{
	public class BookMarkCommandHandler : ResponseHandler,
			IRequestHandler<AddBookMarkCommand, Response<int>>,
			IRequestHandler<DeleteBookmarkByIdCommand, Response<string>>,
			IRequestHandler<DeleteBookmarkByJobIdCommand, Response<string>>
	{

		#region Fields
		private readonly IBookmarkService _bookMarkService;
		private readonly IMapper _mapper;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;
		#endregion

		#region Constructors
		public BookMarkCommandHandler(IBookmarkService bookMarkService, IMapper mapper,
									IStringLocalizer<SharedResources> stringLocalizer,
									IAuthorizationService authorizationService,
									ICurrentUserService currentUserService) : base(stringLocalizer)
		{
			_bookMarkService = bookMarkService;
			_mapper = mapper;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
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

			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				bookmark,
				AuthorizationPolicies.CanAccessOwnBookmarks);

			if (!isAuthorized.Succeeded)
				return Forbidden<string>("Access denied");

			bool IsDeleted = await _bookMarkService.DeleteBookmarkAsync(bookmark);

			if (!IsDeleted) return BadRequest<string>();

			return Deleted<string>();

		}

		public async Task<Response<string>> Handle(DeleteBookmarkByJobIdCommand request, CancellationToken cancellationToken)
		{

			int userId = _currentUserService.GetCurrentUserId();

			var bookmark = await _bookMarkService.GetUserBookmarkAsync(userId, request.Id);

			if (bookmark.UserId != userId) return Forbidden<string>();

			if (bookmark is null) return NotFound<string>();

			bool IsDeleted = await _bookMarkService.DeleteBookmarkAsync(bookmark);

			if (!IsDeleted) return BadRequest<string>();

			return Deleted<string>();
		}


		#endregion

	}
}
