using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Helpers;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Core.Wrapers;
using JobBoard.Data.enums;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Jobs.Queries.Handler
{
	public class JobQueryHandler : ResponseHandler,
			IRequestHandler<GetJobByIdQuery, Response<GetJobByIdQueryResponse>>,
			IRequestHandler<GetPaginatedJobsQuery, PaginatedResponse<List<GetPaginatedJobsQueryResponse>>>,
			IRequestHandler<GetJobSkillsQuery, Response<List<GetJobSkillsQueryResponse>>>,
			IRequestHandler<GetJobCategoriesQuery, Response<List<GetJobCategoriesQueryResponse>>>,
			IRequestHandler<GetJobsByCompanyIdQuery, Response<GetJobsByCompanyIdQueryResponse>>,
			IRequestHandler<GetPopularLocationsQuery, Response<string[]>>,
			IRequestHandler<GetRecommendationJobsQuery, Response<List<JobResponseDto>>>,
			IRequestHandler<GetJobByIdSummaryQuery, Response<GetJobByIdSummaryQueryResponse>>,
			IRequestHandler<GetJobApplicantsSummary, PaginatedResponse<List<GetJobApplicantSummaryResponse>>>
	{

		#region Fields
		private readonly IJobService _jobService;
		private readonly IMapper _mapper;
		private readonly ICompanyService _companyService;
		private readonly IAuthorizationService _authorizationService;
		private readonly ICurrentUserService _currentUserService;
		#endregion

		#region Constructors
		public JobQueryHandler(IJobService jobService,
							IMapper mapper,
							IStringLocalizer<SharedResources> stringLocalizer,
							ICompanyService companyService,
							IAuthorizationService authorizationService,
							ICurrentUserService currentUserService

			) : base(stringLocalizer)
		{
			_jobService = jobService;
			_mapper = mapper;
			_companyService = companyService;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
		}
		#endregion

		#region Handle Methods
		public async Task<Response<GetJobByIdQueryResponse>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
		{
			var job = await _jobService.GetJobByIdWithEncludeAsync(request.Id);
			if (job == null) return NotFound<GetJobByIdQueryResponse>();

			return Success(_mapper.Map<GetJobByIdQueryResponse>(job));
		}

		public async Task<PaginatedResponse<List<GetPaginatedJobsQueryResponse>>> Handle(GetPaginatedJobsQuery request, CancellationToken cancellationToken)
		{
			var queryable = _jobService.GetJobsQueryable().Where(j => j.Status == JobStatusEnum.Active);

			// perform filters and sorting

			var parsedJobTypes = request.JobTypes.SafeParseEnums<JobTypeEnum>().ToArray();
			var parsedExperienceLevels = request.ExperienceLevels.SafeParseEnums<ExperienceLevelEnum>().ToArray();
			queryable = queryable
						.ApplySearch(request.SearchByTitle, request.SearchByLocation)
						.ApplyFilters(parsedJobTypes, parsedExperienceLevels, request.SortBy,
										request.PopularCompanies, request.PopularCategories);

			var result = await _mapper.ProjectTo<GetPaginatedJobsQueryResponse>(queryable)
					.ToPaginatedAsync(request.PageNumber, request.PageSize);

			return result;

		}

		public async Task<Response<List<GetJobSkillsQueryResponse>>> Handle(GetJobSkillsQuery request, CancellationToken cancellationToken)
		{
			bool Exist = await _jobService.IsExistByIdAsync(request.JobId);

			if (!Exist) return NotFound<List<GetJobSkillsQueryResponse>>();

			var skills = await _jobService.GetJobSkillsAsync(request.JobId);

			var skillsMapping = _mapper.Map<List<GetJobSkillsQueryResponse>>(skills);

			return Success(skillsMapping);
		}

		public async Task<Response<List<GetJobCategoriesQueryResponse>>> Handle(GetJobCategoriesQuery request, CancellationToken cancellationToken)
		{
			bool Exist = await _jobService.IsExistByIdAsync(request.JobId);

			if (!Exist) return NotFound<List<GetJobCategoriesQueryResponse>>();

			var Categries = await _jobService.GetJobCategoriesAsync(request.JobId);

			var skillsMapping = _mapper.Map<List<GetJobCategoriesQueryResponse>>(Categries);

			return Success(skillsMapping);
		}

		public async Task<Response<GetJobsByCompanyIdQueryResponse>> Handle(GetJobsByCompanyIdQuery request, CancellationToken cancellationToken)
		{

			var company = _companyService.GetCompanyByIdAsync(request.CompanyId);

			var userRoles = await _currentUserService.GetCurrentUserRoles();

			// of not Admin apply entity based authorization (resource-based-Authorization)

			if (!userRoles.Contains("Admin"))
			{
				var isAuthorized = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(), company, new CompanyOwnerRequirement());

				if (!isAuthorized.Succeeded) return Forbidden<GetJobsByCompanyIdQueryResponse>();

			}



			if (company is null) return NotFound<GetJobsByCompanyIdQueryResponse>();

			var result = await _jobService.GetJobsByCompanyIdAsync(request.CompanyId);

			var JobsDto = _mapper.Map<List<JobResponse>>(result);

			return Success(new GetJobsByCompanyIdQueryResponse { Jobs = JobsDto });
		}

		public async Task<Response<string[]>> Handle(GetPopularLocationsQuery request, CancellationToken cancellationToken)
		{
			var res = await _jobService.GetPopularLocations();
			return Success(res);
		}

		public async Task<Response<List<JobResponseDto>>> Handle(GetRecommendationJobsQuery request, CancellationToken cancellationToken)
		{

			var jobsQueryable = _jobService.GetRecommendationJobs(_currentUserService.GetCurrentUser());


			return Success(await _mapper.ProjectTo<JobResponseDto>(jobsQueryable).ToListAsync());


		}

		public async Task<Response<GetJobByIdSummaryQueryResponse>> Handle(GetJobByIdSummaryQuery request, CancellationToken cancellationToken)
		{
			var job = await _jobService.GetJobByIdWithEncludeAsync(request.Id);
			if (job == null) return NotFound<GetJobByIdSummaryQueryResponse>();

			return Success(_mapper.Map<GetJobByIdSummaryQueryResponse>(job));
		}

		public async Task<PaginatedResponse<List<GetJobApplicantSummaryResponse>>> Handle(GetJobApplicantsSummary request, CancellationToken cancellationToken)
		{
			var applicants = _jobService.GetJobApplicants(request.JobId, request.Filter, request.Sort);

			return (await _mapper.ProjectTo<GetJobApplicantSummaryResponse>(applicants)
											.ToPaginatedAsync(request.Page, request.Size));


		}


		#endregion

	}
}
