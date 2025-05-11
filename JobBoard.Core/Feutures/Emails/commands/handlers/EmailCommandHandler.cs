using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Emails.commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Emails.commands.handlers
{
	public class EmailCommandHandler : ResponseHandler,
							IRequestHandler<SendEmailCommand, Response<string>>


	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IEmailService _emailService;
		#endregion

		#region Constructors
		public EmailCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
								IEmailService emailService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_emailService = emailService;
		}


		#endregion

		#region Handle Methods
		public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
		{
			var result = await _emailService.SendEmail(request.Email, request.Message);

			if (result == "Failed") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedSendEmail]);

			return Success("");

		}
		#endregion
	}
}
