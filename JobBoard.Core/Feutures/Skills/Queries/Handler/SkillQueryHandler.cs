using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Skills.Queries.Models;
using JobBoard.Core.Feutures.Skills.Queries.Results;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Skills.Queries.Handler
{
	public class SkillQueryHandler : ResponseHandler,
									IRequestHandler<GetSingleSkillQuery, Response<GetSingleSkillQueryResponse>>,
									IRequestHandler<GetListSkillsQuery, PaginatedResponse<GetListSkillsQueryResponse>>,
									IRequestHandler<GetSkillsSummaryQuery, PaginatedResponse<GetSkillsSummaryQueryResponse>>
	{
		#region Fields
		private readonly ISkillService _skillService;
		private readonly IMapper _mapper;

		#endregion
		#region Constructors
		public SkillQueryHandler(ISkillService skillService, IMapper mapper,
								IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this._skillService = skillService;
			this._mapper = mapper;
		}

		#endregion

		#region Handlers
		public async Task<Response<GetSingleSkillQueryResponse>> Handle(GetSingleSkillQuery request, CancellationToken cancellationToken)
		{
			var skill = await _skillService.GetSkillByIdAsync(request.Id);
			if (skill == null) return BadRequest<GetSingleSkillQueryResponse>("Skill Not Found");

			return Success(_mapper.Map<GetSingleSkillQueryResponse>(skill));
		}

		public async Task<PaginatedResponse<GetListSkillsQueryResponse>> Handle(GetListSkillsQuery request, CancellationToken cancellationToken)
		{
			var queryable = _skillService.GetSkillsQueryable(request.Search, request.SortBy);

			var skills = await _mapper.ProjectTo<GetListSkillsQueryResponse>(queryable).ToPaginatedAsync(request.Page, request.PageSize);
			return skills;
		}

		public async Task<PaginatedResponse<GetSkillsSummaryQueryResponse>> Handle(GetSkillsSummaryQuery request, CancellationToken cancellationToken)
		{
			var result = _skillService.GetSkillsQueryable();
			return (await _mapper.ProjectTo<GetSkillsSummaryQueryResponse>(result).ToPaginatedAsync(request.page, request.Size));
		}
		#endregion
	}
}
