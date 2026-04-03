namespace JobBoard.Core.Common.DTOs
{
	public class EducationDto
	{

		public int Id { get; set; }

		public string SchoolName { get; set; } = null!;
		public string? Degree { get; set; }
		public string? FieldOfStudy { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public string? Description { get; set; }
	}
}
