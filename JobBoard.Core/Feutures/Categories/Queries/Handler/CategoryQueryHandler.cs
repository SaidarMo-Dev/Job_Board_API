using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Categories.Queries.Models;
using JobBoard.Core.Feutures.Categories.Queries.Results;
using JobBoard.Core.Resources;
using JobBoard.Core.Wrapers;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Categories.Queries.Handler
{
	public class CategoryQueryHandler : ResponseHandler, IRequestHandler<GetSingleCategoryQuery, Response<GetSingleCategoryQueryResponse>>,
										IRequestHandler<GetListCategoriesQuery, PaginatedResponse<List<GetListCategoriesQueryResponse>>>,
										IRequestHandler<GetPopularCategoriesQuery, Response<List<GetPopularCategoriesQueryResponse>>>
	{

		#region Fields
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		#endregion

		#region Constructors
		public CategoryQueryHandler(ICategoryService categoryService, IMapper mapper,
			IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}
		#endregion

		#region Handles
		public async Task<Response<GetSingleCategoryQueryResponse>> Handle(GetSingleCategoryQuery request, CancellationToken cancellationToken)
		{
			var category = await _categoryService.FindById(request.Id);

			if (category == null) return NotFound<GetSingleCategoryQueryResponse>("Not Found");

			return Success(_mapper.Map<GetSingleCategoryQueryResponse>(category));

		}

		public async Task<PaginatedResponse<List<GetListCategoriesQueryResponse>>> Handle(GetListCategoriesQuery request, CancellationToken cancellationToken)
		{
			var queryable = _categoryService.GetCategoriesQueryable(request.Search, request.sort);

			var categories = await _mapper.ProjectTo<GetListCategoriesQueryResponse>(queryable)
									.ToPaginatedAsync(request.Page, request.PageSize);

			return categories;

		}

		public async Task<Response<List<GetPopularCategoriesQueryResponse>>> Handle(GetPopularCategoriesQuery request, CancellationToken cancellationToken)
		{
			var categories = await _categoryService.GetPopularCategoriesAsync();
			var categoriesDto = _mapper.Map<List<GetPopularCategoriesQueryResponse>>(categories);

			return Success(categoriesDto);
		}


		#endregion
	}
}
