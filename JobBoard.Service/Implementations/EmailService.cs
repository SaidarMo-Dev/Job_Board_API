using System.Text.RegularExpressions;
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

		private static string _StripHtml(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return string.Empty;
			return Regex.Replace(input, "<.*?>", string.Empty);
		}


		public async Task<string> SendEmail(string recipientEmail, string recipientName, string htmlMessage, string subject)
		{
			try
			{

				using var client = new SmtpClient();

				await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
				await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);

				var body = new BodyBuilder
				{
					HtmlBody = $@"
				<html>
				<body style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>
					<p>Dear {recipientName},</p>
					{htmlMessage}
					<p>Best regards,<br/>The Saidar Team</p>
				</body>
				</html>",
					TextBody = $"Dear {recipientName},\n\n{_StripHtml(htmlMessage)}\n\nBest regards,\nThe Saidar Team"

				};
				var mMessage = new MimeMessage
				{
					Body = body.ToMessageBody()
				};

				mMessage.From.Add(new MailboxAddress("Saidar Team", _emailSettings.FromEmail));
				mMessage.To.Add(new MailboxAddress("Test User", recipientEmail));

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
