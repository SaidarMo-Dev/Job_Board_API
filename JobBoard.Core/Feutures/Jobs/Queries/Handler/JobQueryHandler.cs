using System.Security.Claims;
using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Security.Requirements;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Jobs.Queries.Handler
{
	public class JobQueryHandler : ResponseHandler,
			IRequestHandler<GetJobByIdQuery, Response<GetJobByIdQueryResponse>>,
			IRequestHandler<GetPaginatedJobsQuery, PaginatedResponse<List<GetPaginatedJobsQueryResponse>>>,
			IRequestHandler<GetJobSkillsQuery, Response<List<GetJobSkillsQueryResponse>>>,
			IRequestHandler<GetJobCategoriesQuery, Response<List<GetJobCategoriesQueryResponse>>>,
			IRequestHandler<GetJobsByCompanyIdQuery, Response<GetJobsByCompanyIdQueryResponse>>
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
			var queryable = _jobService.GetJobsQueryable();

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

		#endregion

	}
}
