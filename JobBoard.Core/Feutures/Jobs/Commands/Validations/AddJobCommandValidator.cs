using FluentValidation;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Core.Feutures.Jobs.Commands.Validations
{
	public class AddJobCommandValidator : AbstractValidator<AddJobCommand>
	{
		private readonly ICompanyService _companyService;
		private readonly ISkillService _skillService;
		private readonly ICategoryService _categoryService;

		public AddJobCommandValidator(ICompanyService companyService,
			ISkillService skillService,
			ICategoryService categoryService)
		{
			_companyService = companyService;
			_skillService = skillService;
			_categoryService = categoryService;

			AddValidations();
			AddCustomValidations();
		}

		public void AddValidations()
		{
			RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Job title is required.")
			.MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

			RuleFor(x => x.Description)
				.MaximumLength(5000).WithMessage("Description is too long.");

			RuleFor(x => x.CompanyId)
				.GreaterThan(0).WithMessage("A valid Company is required.")
				.MustAsync(async (key, cancellationToken) => await _companyService.IsExistByIdAsync(key))
				.WithMessage("There is no Company With The Id you entered");

			RuleFor(x => x.Location)
				.NotEmpty().WithMessage("Location is required.");

			// Enum Validation
			RuleFor(x => x.JobType)
				.IsInEnum().WithMessage("Please select a valid Job Type.");

			RuleFor(x => x.ExperienceLevel)
				.IsInEnum().WithMessage("Please select a valid Experience Level.");

			// Salary Logic
			RuleFor(x => x.MinSalary)
				.GreaterThanOrEqualTo(0).WithMessage("Minimum salary cannot be negative.");

			RuleFor(x => x.MaxSalary)
				.GreaterThanOrEqualTo(x => x.MinSalary)
				.WithMessage("Maximum salary must be greater than or equal to Minimum salary.");

			// Date Logic
			RuleFor(x => x.DateExpired)
				.Must(date => date > DateTime.UtcNow)
				.WithMessage("Expiration date must be in the future.");


		}

		public void AddCustomValidations()
		{
			// Collection Validation
			RuleFor(x => x.skillIds)
				.Must(x => x.Count <= 20)
				.WithMessage("You cannot select more than 20 skills.")
				.MustAsync(async (skillIds, ct) =>
				{
					// If the list is null or empty, it's valid (because skills are optional)
					if (skillIds == null || skillIds.Count == 0) return true;

					var existing = await _skillService.GetSkillsQueryable()
					.CountAsync(s => skillIds.Contains(s.SkillId), ct);

					return existing == skillIds.Count;

				})
				.WithMessage("One or more selected skills are invalid.");

			RuleFor(x => x.CategoryIds)
				.NotEmpty().WithMessage("At least one category is required.")
				.MustAsync(async (categoryIds, ct) =>
				{
					var existing = await _categoryService.GetCategoriesQueryable()
					.CountAsync(c => categoryIds.Contains(c.CategoryId), ct);

					return existing == categoryIds.Count;
				})
				.WithMessage("One or more selected categories are invalid.");

		}
	}
}
