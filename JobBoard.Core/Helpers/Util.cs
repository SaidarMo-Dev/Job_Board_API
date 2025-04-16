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
	}
}
