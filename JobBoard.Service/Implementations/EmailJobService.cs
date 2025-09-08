using Hangfire;
using JobBoard.Core.Helpers;
using JobBoard.Service.Abstractions;
using Serilog;

namespace JobBoard.Service.Implementations
{
	public class EmailJobService : IEmailJobService
	{
		private readonly IBackgroundJobClient _backgroundJobClient;

		public EmailJobService(IBackgroundJobClient backgroundJobClient)
		{
			_backgroundJobClient = backgroundJobClient;
		}

		public void EnqueueVerificationEmail(string toEmail, string fullName, string code)
		{
			try
			{
				_backgroundJobClient.Enqueue<IEmailService>(emailService =>
					emailService.SendEmailAsync(
						toEmail,
						fullName,
						Util.FormatVerificationMessage(code),
						"Email Confirmation")
				);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to enqueue verification email");
			}
		}

		public void EnqueuePasswordResetEmail(string toEmail, string fullName, string resetCode)
		{
			try
			{
				_backgroundJobClient.Enqueue<IEmailService>(emailService =>
					emailService.SendEmailAsync(
						toEmail,
						fullName,
						Util.FormatVerificationMessage(resetCode),
						"Password Reset")
				);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to enqueue password reset email");
			}
		}
	}

}
