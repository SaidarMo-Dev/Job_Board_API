using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Applications.Queries.Handler
{
	public class ApplicationQueryHandler : ResponseHandler,
				IRequestHandler<GetSingleApplicationQuery, Response<GetSingleApplictionQueryResponse>>,
				IRequestHandler<GetApplicationsByJobIdQuery, Response<GetApplicationsByJobIdQueryResponse>>,
				IRequestHandler<GetCurrentUserApplicationsQuery, Response<GetCurrentUserApplicationsQueryResponse>>
	{
		private readonly IApplicationService _applicationService;
		private readonly IMapper _mapper;
		private readonly IJobService _jobService;
		private readonly ICurrentUserService _currentUserService;
		private readonly IAuthorizationService _authorizationService;

		#region Fields 
		#endregion

		#region Constructors 
		public ApplicationQueryHandler(IApplicationService applicationService,
										IMapper mapper,
										IStringLocalizer<SharedResources> stringLocalizer,
										IJobService jobService,
										ICurrentUserService currentUserService,
										IAuthorizationService authorizationService
										) : base(stringLocalizer)
		{
			_applicationService = applicationService;
			_mapper = mapper;
			_jobService = jobService;
			_currentUserService = currentUserService;
			_authorizationService = authorizationService;
		}
		#endregion

		#region Handle Methods
		public async Task<Response<GetSingleApplictionQueryResponse>> Handle(GetSingleApplicationQuery request, CancellationToken cancellationToken)
		{
			var application = await _applicationService.GetByIdWithIncludeAsync(request.Id);

			if (application is null) return NotFound<GetSingleApplictionQueryResponse>();

			return Success(_mapper.Map<GetSingleApplictionQueryResponse>(application));

		}

		public async Task<Response<GetApplicationsByJobIdQueryResponse>> Handle(GetApplicationsByJobIdQuery request, CancellationToken cancellationToken)
		{
			// check the existense of the job

			var exist = await _jobService.IsExistByIdAsync(request.JobId);
			if (!exist) return NotFound<GetApplicationsByJobIdQueryResponse>();


			var applications = await _applicationService.GetApplicationsByJobIdAsync(request.JobId);

			var applicationDto = _mapper.Map<List<ApplicationResponse>>(applications);


			return Success(new GetApplicationsByJobIdQueryResponse { Applications = applicationDto });
		}

		public async Task<Response<GetCurrentUserApplicationsQueryResponse>> Handle(GetCurrentUserApplicationsQuery request, CancellationToken cancellationToken)
		{
			int userId = _currentUserService.GetCurrentUserId();

			var applications = await _applicationService.GetUserApplicationsAsync(userId);

			if (applications is null) return NotFound<GetCurrentUserApplicationsQueryResponse>();

			var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), applications.FirstOrDefault(), new UserApplicationsRequirement());

			if (!isAuthorized.Succeeded) return Forbidden<GetCurrentUserApplicationsQueryResponse>();

			var applicationsDto = _mapper.Map<List<UserApplicationResponse>>(applications);

			return Success(new GetCurrentUserApplicationsQueryResponse { applications = applicationsDto });
		}

		#endregion

	}
}
