using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using MailKit.Net.Smtp;

namespace JobBoard.Service.Implementations
{
	public class EmailService : IEmailService
	{
		#region Fields
		private readonly EmailSettings _emailSettings;

		#endregion

		#region Constructors
		public EmailService(EmailSettings emailSettings)
		{
			_emailSettings = emailSettings;
		}

		#endregion

		#region Methods
		public async Task<string> SendEmail(string email, string message)
		{
			using var client = new SmtpClient();

			await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);


		}

		#endregion

	}
}
