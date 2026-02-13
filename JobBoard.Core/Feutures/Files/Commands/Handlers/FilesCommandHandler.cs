using JobBoard.Core.Authrization.Requirements;
using JobBoard.Core.Authrization.Resources;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Files.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Data.enums;
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
				new FileOwnerRequirement()
				);

			if (!isAuthorized.Succeeded)
			{
				return Forbidden<int>("You are not authorized to upload file for this owner.");
			}

			// Get existing file (if any)
			var existingFile = await _fileResourceService
				.GetByOwnerAsync(request.OwnerType, request.OwnerId);

			// BUSINESS RULE: Application resume is immutable
			if (request.OwnerType == FileOwnerType.Applications && existingFile is not null)
			{
				return BadRequest<int>(
					"Resume cannot be updated after the application is created.");
			}

			string? newPath = null;
			string? oldPath = existingFile?.Path;

			try
			{
				await using var stream = request.File.OpenReadStream();

				// Upload new physical file
				newPath = await _fileStorageService.UploadAsync(
					stream,
					request.File.FileName,
					request.File.ContentType,
					request.OwnerType,
					request.OwnerId,
					request.FilePathType,
					cancellationToken);

				// First-time upload (no existing record)
				if (existingFile is null)
				{
					var fileResource = new FileResource
					{
						Bucket = request.OwnerType.ToString().ToLower(),
						Path = newPath,
						OwnerType = request.OwnerType,
						OwnerId = request.OwnerId,
						Visibility = request.Visibility,
					};

					fileResource = await _fileResourceService.AddAsync(fileResource);
					return Success(fileResource.Id);
				}

				// Replace behavior (User / Company)
				existingFile.Path = newPath;
				existingFile.Visibility = request.Visibility;
				existingFile.CreatedAt = DateTime.UtcNow;

				await _fileResourceService.UpdateAsync(existingFile);

				// Cleanup old physical file AFTER successful update
				if (!string.IsNullOrWhiteSpace(oldPath))
				{
					await _fileStorageService.DeleteAsync(request.OwnerType, oldPath);
				}

				return Success(existingFile.Id);
			}
			catch
			{
				// Rollback new upload if something failed
				if (!string.IsNullOrWhiteSpace(newPath))
				{
					await _fileStorageService.DeleteAsync(request.OwnerType, newPath);
				}

				throw;
			}

		}

		#endregion

	}
}
