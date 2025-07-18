using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Commands.Models;
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

		#endregion

		#region Constructors
		public ApplicationCommandHandler(IApplicationService applicationService, IMapper mapper,
										IStringLocalizer<SharedResources> localizer,
										IJobService jobService) : base(localizer)
		{
			_applicationService = applicationService;
			_mapper = mapper;
			_localizer = localizer;
			_jobService = jobService;
		}

		#endregion

		#region Handle Methods
		public async Task<Response<int>> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
		{
			var jobExist = await _jobService.IsExistByIdAsync(request.JobId);

			if (!jobExist) return BadRequest<int>("Job not found");
			var hasApp = await _applicationService.HasActiveOrAcceptedApplicationAsnycWithJob(request.UserId, request.JobId);

			if (hasApp) return BadRequest<int>(_localizer[SharedResourcesKeys.UserHasActiveApplication]);

			var application = _mapper.Map<Application>(request);

			// TODO : handle saving resume to cloud storage 

			application.ResumeUrl = "Test Url";
			var success = await _applicationService.AddAsync(application);

			if (!success) return BadRequest<int>(_localizer[SharedResourcesKeys.FailedAddApplication]);

			return Created(application.ApplicationId);

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

			application.status = ApplicationStatusEnum.Accepted;
			application.LastStatusDate = DateTime.UtcNow;

			return await PerformUpdateApplicationAsync(application);
		}

		public async Task<Response<string>> Handle(SetApplicationStatusToRemovedCommand request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdAsync(request.ApplicationId);
			if (application is null) return NotFound<string>();

			application.status = ApplicationStatusEnum.Rejected;
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
			var succeded = await _applicationService.UpdateAsnyc(application);

			if (!succeded) return BadRequest<string>(_localizer[SharedResourcesKeys.FailedUpdateApplication]);

			return Success<string>();
		}
		#endregion


	}
}
