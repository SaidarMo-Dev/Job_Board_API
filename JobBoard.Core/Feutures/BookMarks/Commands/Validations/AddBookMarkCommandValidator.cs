using FluentValidation;
using JobBoard.Core.Feutures.BookMarks.Commands.Models;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.BookMarks.Commands.Validations
{
	public class AddBookMarkCommandValidator : AbstractValidator<AddBookMarkCommand>
	{
		private readonly IJobService _jobService;
		private readonly IUserService _userService;

		public AddBookMarkCommandValidator(IJobService jobService, IUserService userService)
		{
			_jobService = jobService;
			_userService = userService;

			AddValidations();
		}

		public void AddValidations()
		{
			RuleFor(x => x.JobId)
				.MustAsync(async (key, cancellationToken) => await _jobService.IsExistByIdAsync(key))
				.WithMessage("The specifec Job Not Found!");

			RuleFor(x => x.UserId)
				.MustAsync(async (key, cancellationToken) => await _userService.IsExistByIdAsync(key))
				.WithMessage("User Not Found!");

		}

	}
}
