using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Files.Queries.Models;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Core.Security.Resources;
using JobBoard.Data.enums;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Files.Queries.Handlers
{
	public class GenerateFileAccessUrlHandler
	:
		ResponseHandler,
		IRequestHandler<GenerateFileAccessUrlQuery, Response<string>>
	{

		#region Fields
		private readonly IFileResourceService _fileResourceService;
		private readonly IFileStorageService _storageService;
		private readonly ICurrentUserService _currentUserService;
		private readonly IAuthorizationService _aspNetauthorizationService;
		#endregion

		#region Constructors
		public GenerateFileAccessUrlHandler(
			IStringLocalizer<SharedResources> stringLocalizer,
			IFileResourceService fileResourceService,
			IFileStorageService fileStorageService,
			ICurrentUserService currentUserService,
			IAuthorizationService aspNetauthorizationService
			)
			: base(stringLocalizer)
		{
			_fileResourceService = fileResourceService;
			_storageService = fileStorageService;
			_currentUserService = currentUserService;
			_aspNetauthorizationService = aspNetauthorizationService;
		}
		#endregion


		#region Handlers
		public async Task<Response<string>> Handle(
			GenerateFileAccessUrlQuery request,
			CancellationToken cancellationToken)
		{
			var file = await _fileResourceService.GetByIdAsync(request.FileResourceId);

			if (file is null)
				return NotFound("File not found.");


			var bucket = _storageService.GetBucket(file.OwnerType);

			// Public files are accessible without any permissions 
			if (file.Visibility == FileVisibility.Public)
			{
				var publicUrl = _storageService.GetPublicUrl(bucket, file.Path);
				return Success(publicUrl);
			}

			// Verify authorization for private files
			var uploadResource = new FileUploadResource
			{
				OwnerType = file.OwnerType,
				OwnerId = file.OwnerId
			};

			var isAuthorized = await _aspNetauthorizationService.AuthorizeAsync(
				_currentUserService.GetCurrentUserPrincipal(),
				uploadResource,
				new FileOwnershipRequirement());


			if (!isAuthorized.Succeeded)
				return Forbidden<string>("Access denied.");

			var signedUrl = await _storageService.CreateSignedReadUrlAsync(
				bucket,
				file.Path);

			return Success(signedUrl);
		}
		#endregion
	}

}
