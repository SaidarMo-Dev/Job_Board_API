using FluentValidation;
using JobBoard.Core.Feutures.Skills.Commands.Models;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.Skills.Commands.Validations
{
	public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
	{
		private readonly ISkillService _skillService;

		public UpdateSkillCommandValidator(ISkillService skillService)
		{
			_skillService = skillService;
			AddValidations();
			AddCustomValidations();
		}

		public void AddValidations()
		{
			RuleFor(x => x.Name)
				.NotNull().WithMessage("{PropertyName} Cannot Be Null!")
				.NotEmpty().WithMessage("{PropertyName} Cannot Be Empty!");

		}
		public void AddCustomValidations()
		{
			RuleFor(x => x.Name)
				.MustAsync(async (obj, key, cancellationToken) => !await _skillService.IsExistByNameExcludeSelfAsync(obj.SkillId, key))
				.WithMessage("{PropertyName} Already Exist!");

		}
	}
}
