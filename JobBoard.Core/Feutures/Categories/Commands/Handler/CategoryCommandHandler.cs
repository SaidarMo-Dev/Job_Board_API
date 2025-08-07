using AutoMapper;
using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Categories.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Data.Entities;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Categories.Commands.Handler
{
	public class CategoryCommandHandler : ResponseHandler,
					IRequestHandler<AddCategoryCommand, Response<int>>,
					IRequestHandler<UpdateCategoryCommand, Response<string>>,
					IRequestHandler<DeleteCategoryCommand, Response<string>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		#region Fields

		#endregion


		#region Constructors
		public CategoryCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
									 ICategoryService categoryService,
									IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_categoryService = categoryService;
			_mapper = mapper;
		}


		#endregion

		#region Handles
		public async Task<Response<int>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
		{
			var newCategory = _mapper.Map<Category>(request);

			var date = DateTime.Now;

			newCategory.CreateDate = new DateOnly(date.Year, date.Month, date.Day);

			var result = await _categoryService.AddAsync(newCategory);

			return Created(result);
		}

		public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _categoryService.FindById(request.CategoryId);

			if (category is null) return NotFound<string>();

			var newCategory = _mapper.Map(request, category);

			await _categoryService.UpdateAsync(newCategory);

			return Success<string>();
		}

		public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _categoryService.FindById(request.Id);
			if (category is null) return NotFound<string>();

			await _categoryService.DeleteAsync(category);

			return Success<string>();
		}

		#endregion
	}
}
