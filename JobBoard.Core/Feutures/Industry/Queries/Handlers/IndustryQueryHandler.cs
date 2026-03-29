using AutoMapper;
using JobBoard.Core.Feutures.Industry.Queries.Models;
using JobBoard.Core.Feutures.Industry.Queries.Responses;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Core.Feutures.Industry.Queries.Handlers
{


	public class GetIndustriesQueryHandler
		: IRequestHandler<GetIndustriesQuery, PaginatedResponse<GetIndustriesQueryResponse>>
	{
		private readonly IIndustryService _industryService;
		private readonly IMapper _mapper;

		public GetIndustriesQueryHandler(IIndustryService industryService,
			IMapper mapper)
		{
			_industryService = industryService;
			_mapper = mapper;
		}

		public async Task<PaginatedResponse<GetIndustriesQueryResponse>> Handle(
			GetIndustriesQuery request,
			CancellationToken cancellationToken)
		{
			var query = _industryService.GetIndustriesQueryable();

			// Apply search
			if (!string.IsNullOrWhiteSpace(request.Search))
			{
				var pattern = $"%{request.Search.Trim()}%";

				query = query.Where(i =>
					EF.Functions.Like(i.Name, pattern) ||
					EF.Functions.Like(i.Slug, pattern));
			}

			var industries = await _mapper.ProjectTo<GetIndustriesQueryResponse>(query)
				.ToPaginatedAsync(request.PageNumber, request.PageSize);

			return industries;
		}
	}
}
