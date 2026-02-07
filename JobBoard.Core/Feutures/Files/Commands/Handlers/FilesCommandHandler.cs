using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Files.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Core.Security.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Files.Commands.Handlers
{
	public class FilesCommandHandler : ResponseHandler,
		IRequestHandler<UploadFileCommand, Response<int>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IFileStorageService _fileStorageService;
		private readonly IFileResourceService _fileResourceService;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;

		#endregion

		#region Constructors
		public FilesCommandHandler(
			IStringLocalizer<SharedResources> stringLocalizer,
			IFileStorageService fileStorageService,
			IFileResourceService fileResourceService,
			IAuthorizationService authorizationService,
			ICurrentUserService currentUserService

			)

			: base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_fileStorageService = fileStorageService;
			_fileResourceService = fileResourceService;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
		}
		#endregion

		#region Handlers
		public async Task<Response<int>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
		{

			var uploadResource = new FileUploadResource
			{
				OwnerType = request.OwnerType,
				OwnerId = request.OwnerId,

			};



			// Check if the current user is authorized to upload for this owner
			var isAuthorized = await _authorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				uploadResource,
				new FileOwnershipRequirement()
				);

			if (!isAuthorized.Succeeded)
			{
				return Forbidden<int>("You are not authorized to upload file for this owner.");
			}


			string? path = null;
			try
			{


				await using var stream = request.File.OpenReadStream();

				// Upload the file to storage 
				path = await _fileStorageService.UploadAsync(
					stream,
					request.File.FileName,
					request.File.ContentType,
					request.OwnerType,
					request.OwnerId,
					request.FilePathType,
					cancellationToken);


				// Create and save a FileResource record

				var fileResource = new FileResource
				{
					Bucket = request.OwnerType.ToString().ToLower(),
					Path = path,
					OwnerType = request.OwnerType,
					OwnerId = request.OwnerId,
					Visibility = request.Visibility,
				};

				fileResource = await _fileResourceService.AddAsync(fileResource);


				return Success(fileResource.Id);
			}
			catch
			{

				if (path != null)
				{
					await _fileStorageService.DeleteAsync(path);
				}
				throw;
			}

		}

		#endregion

	}
}
