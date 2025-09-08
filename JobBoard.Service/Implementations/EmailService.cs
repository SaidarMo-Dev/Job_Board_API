using JobBoard.Core.Helpers;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.Helpers;
using JobBoard.Service.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;

namespace JobBoard.Service.Implementations
{
	public class EmailService : IEmailService
	{
		#region Fields
		private readonly EmailSettings _emailSettings;
		private readonly UserManager<User> _userManager;
		private readonly IEmailJobService _emailJobService;

		#endregion

		#region Constructors
		public EmailService(EmailSettings emailSettings, UserManager<User> userManager, IEmailJobService emailJobService)
		{
			_emailSettings = emailSettings;
			_userManager = userManager;
			_emailJobService = emailJobService;
		}

		#endregion

		#region Methods




		public async Task<string> SendEmailAsync(string recipientEmail, string recipientName, string htmlMessage, string subject)
		{
			try
			{

				using var client = new SmtpClient();

				await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);

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
					TextBody = $"Dear {recipientName},\n\n{Util.StripHtml(htmlMessage)}\n\nBest regards,\nThe Saidar Team"

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

		public async Task<(bool Success, string Message)> ResendVerificationCodeAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return (false, "User not found.");

			if (user.EmailConfirmed)
				return (false, "Email already confirmed.");

			// generate new code
			user.Code = Util.GenerateSixDigitCode();
			await _userManager.UpdateAsync(user);

			_emailJobService.EnqueueVerificationEmail(email, user.FullName, Util.FormatVerificationMessage(user.Code));

			return (true, "Verification code resent successfully.");
		}


		#endregion

	}
}
