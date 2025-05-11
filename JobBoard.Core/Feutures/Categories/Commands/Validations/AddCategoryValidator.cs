using FluentValidation;
using JobBoard.Core.Feutures.Categories.Commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Categories.Commands.Validations
{
	public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
	{
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly ICategoryService _categoryService;

		public AddCategoryValidator(IStringLocalizer<SharedResources> localizer,
									ICategoryService categoryService)
		{
			_localizer = localizer;
			_categoryService = categoryService;

			ApplyValidation();
		}

		public void ApplyValidation()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.CategoryNameNotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.CategoryNameNotNull])
				.MustAsync(async (key, cancellationToken) => !await _categoryService.IsNameExistAsync(key))
				.WithMessage(_localizer[SharedResourcesKeys.CategoryNameExist]);
		}

	}
}
