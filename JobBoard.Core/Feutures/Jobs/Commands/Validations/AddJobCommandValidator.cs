using FluentValidation;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.Jobs.Commands.Validations
{
	public class AddJobCommandValidator : AbstractValidator<AddJobCommand>
	{
		private readonly ICompanyService _companyService;

		public AddJobCommandValidator(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		public void AddValidation()
		{
			RuleFor(x => x.Title)
					.NotNull().WithMessage("Title Cannot be Null")
					.NotEmpty().WithMessage("Title Cannot be Empty");


			RuleFor(x => x.CompanyId)
				.MustAsync(async (key, cancellationToken) => await _companyService.IsExistByIdAsync(key))
				.WithMessage("There is no Company With The Id you entered");
		}
	}
}
