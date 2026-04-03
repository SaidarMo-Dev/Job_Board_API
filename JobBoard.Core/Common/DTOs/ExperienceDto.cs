namespace JobBoard.Core.Common.DTOs
{
	public class ExperienceDto
	{

		public int Id { get; set; }
		public string JobTitle { get; set; } = null!;
		public string CompanyName { get; set; } = null!;
		public string Description { get; set; } = null!;
		public bool IsCurrent { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public List<string> Skills { get; set; } = new();

	}
}
