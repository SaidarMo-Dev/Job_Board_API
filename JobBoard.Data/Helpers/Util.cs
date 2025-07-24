using System.Text.RegularExpressions;
using JobBoard.Data.Entities.Identity;

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
				<div style='font-size: 24px; font-weight: bold; color: #039BE5; margin: 20px 0;'>
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

		public static int CalculateProfileCompletion(User user)
		{
			if (user is null) throw new ArgumentNullException("user");

			var fields = new List<(object? value, double weight)>
			{
				(user.FirstName, 1),
				(user.LastName, 1),
				(user.Email, 1),
				(user.PhoneNumber, 1),
				// i want image path to be null for testing i will change it later 
				(null, 2),
				(user.Address, 1),
				(user.Gender, 1),
				(user.CountryId, 0.5),
				(user.DateOfBirth, 0.5),
				(user.UserName, 1),
			};

			var totalWeights = fields.Sum(x => x.weight);

			var completedWeights = fields.Where(x => x.value != null &&
									!string.IsNullOrWhiteSpace(x.value.ToString()))
								.Sum(x => x.weight);
			return (int)((completedWeights / totalWeights) * 100);


		}
	}
}
