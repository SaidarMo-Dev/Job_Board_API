using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using MailKit.Net.Smtp;
using MimeKit;

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
			try
			{

				using var client = new SmtpClient();

				await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
				await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);

				var body = new BodyBuilder
				{
					HtmlBody = message,
					TextBody = "Welcome"

				};
				var mMessage = new MimeMessage
				{
					Body = body.ToMessageBody()
				};

				mMessage.From.Add(new MailboxAddress("Saidar Team", _emailSettings.FromEmail));
				mMessage.To.Add(new MailboxAddress("Test User", email));

				mMessage.Subject = "New Testing Message";


				await client.SendAsync(mMessage);

				await client.DisconnectAsync(true);

				return "Success";
			}
			catch
			{
				return "Failed";
			}
		}

		public async Task<string> SendEmail(string email, string message, string subject)
		{
			try
			{

				using var client = new SmtpClient();

				await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
				await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);

				var body = new BodyBuilder
				{
					HtmlBody = message,
					TextBody = "Welcome"

				};
				var mMessage = new MimeMessage
				{
					Body = body.ToMessageBody()
				};

				mMessage.From.Add(new MailboxAddress("Saidar Team", _emailSettings.FromEmail));
				mMessage.To.Add(new MailboxAddress("Test User", email));

				mMessage.Subject = subject;


				await client.SendAsync(mMessage);

				await client.DisconnectAsync(true);

				return "Success";
			}
			catch
			{
				return "Failed";
			}
		}

		#endregion

	}
}
