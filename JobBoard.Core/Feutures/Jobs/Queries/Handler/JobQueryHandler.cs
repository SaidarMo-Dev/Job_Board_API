using AutoMapper;
using JobBoard.Core.Authorization.Policies;
using JobBoard.Core.Bases;
using JobBoard.Core.Common.DTOs;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Helpers;
using JobBoard.Core.Resources;
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
		private readonly IFileStorageService _storageService;
		private readonly IApplicationService _applicationService;
		#endregion

		#region Constructors
		public JobQueryHandler(IJobService jobService,
							IMapper mapper,
							IStringLocalizer<SharedResources> stringLocalizer,
							ICompanyService companyService,
							IAuthorizationService authorizationService,
							ICurrentUserService currentUserService,
							IFileStorageService storageService,
							IApplicationService applicationService

			) : base(stringLocalizer)
		{
			_jobService = jobService;
			_mapper = mapper;
			_companyService = companyService;
			_authorizationService = authorizationService;
			_currentUserService = currentUserService;
			_storageService = storageService;
			_applicationService = applicationService;
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
			var queryable = _jobService.GetJobsQueryable().Where(j => j.Status == JobStatusEnum.Active && j.DateExpired > DateTime.UtcNow);

			// perform filters and sorting

			var parsedJobTypes = request.JobTypes.SafeParseEnums<JobTypeEnum>().ToArray();
			var parsedExperienceLevels = request.ExperienceLevels.SafeParseEnums<ExperienceLevelEnum>().ToArray();
			queryable = queryable
						.ApplySearch(request.SearchByTitle, request.SearchByLocation)
						.ApplyFilters(parsedJobTypes, parsedExperienceLevels, request.SortBy,
										request.PopularCompanies, request.PopularCategories);

			var result = await _mapper.ProjectTo<GetPaginatedJobsQueryResponse>(queryable)
					.ToPaginatedAsync(request.PageNumber, request.PageSize);

			foreach (var job in result.data)
			{

				if (!string.IsNullOrEmpty(job.Company.LogoUrl))
				{
					job.Company.LogoUrl =
						_storageService.GetPublicUrl(
							_storageService.GetBucket(FileOwnerType.Companies),
							job.Company.LogoUrl);
				}

			}

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
			if (company is null) return NotFound<GetJobsByCompanyIdQueryResponse>();

			var isAuthorized = await _authorizationService.AuthorizeAsync(
					_currentUserService.GetCurrentUserPrincipal(),
					company,
					AuthorizationPolicies.IsCompanyCreator);

			if (!isAuthorized.Succeeded)
				return Forbidden<GetJobsByCompanyIdQueryResponse>();

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
			var applicants = _applicationService.GetJobApplicants(request.JobId, request.Filter, request.Sort);

			var result = await _mapper.ProjectTo<GetJobApplicantSummaryResponse>(applicants)
											.ToPaginatedAsync(request.Page, request.Size);

			if (result.data == null)
				return result;


			var fileIds = result.data
				.Where(app => app.ProfileImageFileId.HasValue)
				.Select(app => app.ProfileImageFileId!.Value);

			var signedUrls = await _storageService.CreateSignedReadUrlsAsync(
				_storageService.GetBucket(FileOwnerType.Applications), fileIds);

			foreach (var applicant in result.data)
			{
				if (applicant.ProfileImageFileId.HasValue &&
					signedUrls.TryGetValue(applicant.ProfileImageFileId.Value, out var url))
				{
					applicant.ProfileImageUrl = url;
				}
			}


			return result;
		}


		#endregion

	}
}
