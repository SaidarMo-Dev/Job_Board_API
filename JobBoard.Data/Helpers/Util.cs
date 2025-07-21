using System.Text.RegularExpressions;

namespace JobBoard.Core.Helpers
{
	public static class Util
	{
		public static bool IsValideEmail(string email)
		{
			string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

			return Regex.IsMatch(email, EmailPattern);
		}

		public static bool IsValidePhoneNumber(string phoneNumber)
		{
			string PhonePattern = @"\+\d{2,}\s\d{6,12}";

			return Regex.IsMatch(phoneNumber, PhonePattern);
		}
		public static bool IsValideUrl(string Url)
		{
			string UrlPattern = @"(https?:\/\/)?(www.)?[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
			return Regex.IsMatch(Url, UrlPattern);
		}

		public static string FormatVerificationMessage(string code)
		{

			string htmlMessage = $@"
				<p>We received a request that requires verification.</p>
				<p>Your verification code is:</p>
				<div style='font-size: 24px; font-weight: bold; color: #2c3e50; margin: 20px 0;'>
					{code}
				</div>
				<p>This code is valid for 10 minutes. If you did not request this, please ignore this email.</p>
			";

			return htmlMessage;

		}

		public static string FormatVerificationLink(string verificationUrl)
		{

			string htmlMessage = $@"
				<p>Dear User,</p>

				<p>Thank you for registering with us. Please verify your email address by clicking the link below:</p>

				<p>
					<a href='{verificationUrl}' style='
						display: inline-block;
						padding: 10px 20px;
						background-color: #1a73e8;
						color: white;
						text-decoration: none;
						border-radius: 5px;
						font-weight: bold;
					'>Verify Email</a>
				</p>

				<p>If the button doesn't work, you can also copy and paste the following link into your browser:</p>
				<p><a href='{verificationUrl}'>{verificationUrl}</a></p>

				<p>Best regards,<br/>The Saidar Team</p>
			";

			return htmlMessage;

		}

		public static string GenerateSixDigitCode()
		{
			var random = new Random();

			return random.Next(0, 100000).ToString("D6");

		}
	}
}
