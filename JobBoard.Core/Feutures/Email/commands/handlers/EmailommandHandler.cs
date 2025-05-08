using JobBoard.Core.Bases;
using JobBoard.Core.Feutures.Email.commands.Models;
using JobBoard.Core.Resources;
using JobBoard.Service.Abstractions;
using MediatR;
using Microsoft.Extensions.Localization;

namespace JobBoard.Core.Feutures.Email.commands.handlers
{
	public class EmailommandHandler : ResponseHandler,
							IRequestHandler<SendEmailCommand, Response<string>>


	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IEmailService _emailService;
		#endregion

		#region Constructors
		public EmailommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
								IEmailService emailService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_emailService = emailService;
		}


		#endregion

		#region Handle Methods
		public Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
		{
			var result = _emailService.SendEmail(request.Email, request.Message);


		}
		#endregion
	}
}
