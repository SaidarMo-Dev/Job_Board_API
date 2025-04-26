using FluentValidation;
using JobBoard.Core.Feutures.Jobs.Commands.Models;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.Jobs.Commands.Validations
{
	public class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
	{
		private readonly ICompanyService _companyService;

		public UpdateJobCommandValidator(ICompanyService companyService)
		{
			_companyService = companyService;
			AddValidations();
		}

		public void AddValidations()
		{
			RuleFor(x => x.Title)
					.NotNull().WithMessage("Title Cannot be Null")
					.NotEmpty().WithMessage("Title Cannot be Empty");
		}
	}
}

