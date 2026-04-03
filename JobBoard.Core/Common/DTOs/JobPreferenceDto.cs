namespace JobBoard.Core.Common.DTOs
{
	public class JobPreferenceDto
	{
		public string? DesiredJobTitle { get; set; }

		public decimal? DesiredSalaryMin { get; set; }
		public decimal? DesiredSalaryMax { get; set; }

		public string? PreferredLocation { get; set; }
		public string? WorkType { get; set; }

		public bool IsOpenToWork { get; set; }
	}
}
