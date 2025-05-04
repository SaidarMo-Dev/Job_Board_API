using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Jobs.Queries.Models;
using JobBoard.Core.Feutures.Jobs.Queries.Responses;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Jobs.Queries.Handler
{
	public class JobQueryHandler : ResponseHandler,
			IRequestHandler<GetJobByIdQuery, Response<GetJobByIdQueryResponse>>,
			IRequestHandler<GetPaginatedJobsQuery, PaginatedResponse<List<GetPaginatedJobsQueryResponse>>>,
			IRequestHandler<GetJobSkillsQuery, Response<List<GetJobSkillsQueryResponse>>>,
			IRequestHandler<GetJobCategoriesQuery, Response<List<GetJobCategoriesQueryResponse>>>
	{
		private readonly IJobService _jobService;
		private readonly IMapper _mapper;
		#region Fields
		#endregion

		#region Constructors
		public JobQueryHandler(IJobService jobService, IMapper mapper,
			IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_jobService = jobService;
			_mapper = mapper;
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

		#endregion

	}
}
