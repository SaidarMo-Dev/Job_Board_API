using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Applications.Queries.Models;
using JobBoard.Core.Feutures.Applications.Queries.Responses;
using JobBoard.Core.Helpers;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Applications.Queries.Handler
{
	public class ApplicationQueryHandler : ResponseHandler,
				IRequestHandler<GetSingleApplicationQuery, Response<GetSingleApplictionQueryResponse>>,
				IRequestHandler<GetApplicationsByJobIdQuery, Response<GetApplicationsByJobIdQueryResponse>>,
				IRequestHandler<GetCurrentUserApplicationsQuery, PaginatedResponse<List<GetCurrentUserApplicationsQueryResponse>>>,
				IRequestHandler<GetRecentApplicationsQuery, Response<IReadOnlyList<GetRecentApplicationsQueryResponse>>>,
				IRequestHandler<GetAppliedJobIdsQuery, Response<int[]>>
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

		public async Task<PaginatedResponse<List<GetCurrentUserApplicationsQueryResponse>>> Handle(GetCurrentUserApplicationsQuery request, CancellationToken cancellationToken)
		{
			int userId = _currentUserService.GetCurrentUserId();

			var applications = _applicationService.GetUserApplicationsQueryable(userId)
							.FilterUserApplications(request.StatusFilter);

			var applicationsDto = _mapper.ProjectTo<GetCurrentUserApplicationsQueryResponse>(applications);

			Console.WriteLine(applicationsDto.ToQueryString());

			var result = await applicationsDto.ToPaginatedAsync(request.Page, request.Size);

			return result;
		}

		public async Task<Response<IReadOnlyList<GetRecentApplicationsQueryResponse>>> Handle(GetRecentApplicationsQuery request, CancellationToken cancellationToken)
		{
			var recentApplications = await _applicationService.GetRecentApplicationsAsync(_currentUserService.GetCurrentUserId(), request.Take);

			return Success(_mapper.Map<IReadOnlyList<GetRecentApplicationsQueryResponse>>(recentApplications));
		}

		public async Task<Response<int[]>> Handle(GetAppliedJobIdsQuery request, CancellationToken cancellationToken)
		{
			var result = await _applicationService.GetAppliedJobIds(_currentUserService.GetCurrentUserId());

			return Success(result);
		}

		#endregion

	}
}
