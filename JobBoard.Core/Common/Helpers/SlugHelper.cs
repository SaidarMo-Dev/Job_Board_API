using System.Text.RegularExpressions;

namespace JobBoard.Core.Common.Helpers
{
	public static class SlugHelper
	{
		public static string Normalize(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return string.Empty;

			// Lowercase and Trim
			string slug = input.ToLowerInvariant().Trim();

			// Replace spaces and underscores with hyphens
			slug = slug.Replace(" ", "-").Replace("_", "-");

			// Remove all characters that are NOT a-z, 0-9, or -
			slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9-]", "");

			// Remove multiple hyphens in a row (e.g., "apple---google" -> "apple-google")
			slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");

			// Trim hyphens from the ends
			return slug.Trim('-');
		}

		public static bool IsValidSlug(string slug)
		{
			var regex = new Regex("^[a-z0-9]+(?:-[a-z0-9]+)*$");
			return regex.IsMatch(slug);
		}
	}
}
