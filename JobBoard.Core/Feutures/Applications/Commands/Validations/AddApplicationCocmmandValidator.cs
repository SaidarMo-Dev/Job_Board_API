using FluentValidation;
using JobBoard.Core.Feutures.Applications.Commands.Models;
using JobBoard.Service.Abstractions;

namespace JobBoard.Core.Feutures.Applications.Commands.Validations
{
	public class AddApplicationCocmmandValidator : AbstractValidator<AddApplicationCommand>
	{

		#region Fields
		private readonly IUserService _userService;
		private readonly IJobService _jobService;

		#endregion

		#region Constructors
		public AddApplicationCocmmandValidator(IUserService userService,
												IJobService jobService)
		{
			_userService = userService;
			_jobService = jobService;

			AddValidations();
		}
		#endregion

		#region Methods

		public void AddValidations()
		{
			RuleFor(x => x.JobId)
				.MustAsync(async (key, cancellationToken) => await _jobService.IsExistByIdAsync(key))
				.WithMessage("Job Not Found");

			RuleFor(x => x.UserId)
				.MustAsync(async (key, cancellationToken) => await _userService.IsExistByIdAync(key))
				.WithMessage("User Not Found");
		}

		#endregion
	}

}
