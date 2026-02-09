using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Commands.Models;
using JobBoard.Core.Feutures.Files.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Applications.Commands.Handler
{
	public class ApplicationCommandHandler : ResponseHandler,
					IRequestHandler<AddApplicationCommand, Response<int>>,
					IRequestHandler<UpdateApplicationCommand, Response<string>>,
					IRequestHandler<SetApplicationStatusToAcceptedCommand, Response<string>>,
					IRequestHandler<SetApplicationStatusToRemovedCommand, Response<string>>,
					IRequestHandler<DeleteApplicationCommand, Response<string>>
	{
		#region Fields
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IJobService _jobService;
		private readonly IMediator _mediator;

		#endregion

		#region Constructors
		public ApplicationCommandHandler(IApplicationService applicationService, IMapper mapper,
			IStringLocalizer<SharedResources> localizer,
			IJobService jobService,
			IMediator mediator

			) : base(localizer)
		{
			_applicationService = applicationService;
			_mapper = mapper;
			_localizer = localizer;
			_jobService = jobService;
			_mediator = mediator;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<int>> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
		{
			// Check job existence

			var jobExist = await _jobService.IsExistByIdAsync(request.JobId);

			if (!jobExist) return BadRequest<int>("Job not found");

			// Check if the user has already an active application for the current job 


			if (await _applicationService
				.HasActiveOrAcceptedApplicationWithJobAsync(
				request.UserId, request.JobId)
				)

				return BadRequest<int>("User Has Active Application");

			var application = _mapper.Map<Application>(request);

			// Save application first 
			var success = await _applicationService.AddAsync(application);

			if (!success) return BadRequest<int>(_localizer[SharedResourcesKeys.FailedAddApplication]);

			Response<int> uploadResult;
			try
			{


				// Upload resume using ApplicationId as owner

				uploadResult = await _mediator.Send(
					new UploadFileCommand(
						request.resume,
						FileOwnerType.Applications,
						application.ApplicationId,
						FileVisibility.Private,
						FilePathType.UuidFileName
					)
				);

				// Link the uploaded resume to the application
				if (uploadResult.statusCode == System.Net.HttpStatusCode.OK)
				{
					application.ResumeFileId = uploadResult.data;
					await _applicationService.UpdateAsync(application);
				}
				else
					throw new Exception($"Resume upload failed.");


				return Created(application.ApplicationId);
			}
			catch
			{
				await _applicationService.DeleteAsync(application);
				throw;
			}

		}

		public async Task<Response<string>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdAsync(request.ApplicationId);
			if (application is null) return NotFound<string>();

			var newApp = _mapper.Map(request, application);

			return await PerformUpdateApplicationAsync(application);

		}

		public async Task<Response<string>> Handle(SetApplicationStatusToAcceptedCommand request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdAsync(request.ApplicationId);
			if (application is null) return NotFound<string>();

			application.Status = ApplicationStatusEnum.Accepted;
			application.LastStatusDate = DateTime.UtcNow;

			return await PerformUpdateApplicationAsync(application);
		}

		public async Task<Response<string>> Handle(SetApplicationStatusToRemovedCommand request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdAsync(request.ApplicationId);
			if (application is null) return NotFound<string>();

			application.Status = ApplicationStatusEnum.Rejected;
			application.LastStatusDate = DateTime.UtcNow;

			return await PerformUpdateApplicationAsync(application);
		}

		public async Task<Response<string>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
		{
			var app = await _applicationService.GetByIdAsync(request.Id);
			if (app is null) return NotFound<string>();

			await _applicationService.DeleteAsync(app);

			return Success<string>();

		}

		private async Task<Response<string>> PerformUpdateApplicationAsync(Application application)
		{
			var succeded = await _applicationService.UpdateAsync(application);

			if (!succeded) return BadRequest<string>(_localizer[SharedResourcesKeys.FailedUpdateApplication]);

			return Success<string>();
		}
		#endregion


	}
}
