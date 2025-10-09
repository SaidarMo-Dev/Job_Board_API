using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Employer.Queries.Models;
using JobBoard.Core.Feutures.Employer.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Employer.Queries.Handlers
{
	public class EmployerQueryHandler : ResponseHandler,
							IRequestHandler<GetEmployerDashboardStatsQuery, Response<GetEmployerDashboardStatsQueryResponse>>,
							IRequestHandler<GetEmployerPostedJobsQuery, PaginatedResponse<List<GetEmployerPostedJobsQueryResponse>>>
	{

		#region Fields
		private readonly IJobService _jobService;
		private readonly IUserService _userService;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		#endregion

		#region Constructors
		public EmployerQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
									IJobService jobService, IUserService userService,
									ICurrentUserService currentUserService,
									IMapper mapper) : base(stringLocalizer)
		{
			_jobService = jobService;
			_userService = userService;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}

		#endregion

		#region Handles
		public async Task<Response<GetEmployerDashboardStatsQueryResponse>> Handle(GetEmployerDashboardStatsQuery request, CancellationToken cancellationToken)
		{
			var result = await _userService.GetEmployerDashboardStats(_currentUserService.GetCurrentUserId());

			return Success(_mapper.Map<GetEmployerDashboardStatsQueryResponse>(result));
		}

		public async Task<PaginatedResponse<List<GetEmployerPostedJobsQueryResponse>>> Handle(GetEmployerPostedJobsQuery request, CancellationToken cancellationToken)
		{
			var jobs = _jobService.GetEmployerPostedJobsQueryable(_currentUserService.GetCurrentUserId(), request.Search);


			return (await _mapper.ProjectTo<GetEmployerPostedJobsQueryResponse>(jobs).ToPaginatedAsync(request.Page, request.Size));
		}

		#endregion


	}
}
