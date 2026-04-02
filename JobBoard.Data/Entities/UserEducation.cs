using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserEducation
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string SchoolName { get; set; } = null!;
		public string? Degree { get; set; }
		public string? FieldOfStudy { get; set; }

		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public string? Description { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }


		//Navigation
		public User User { get; set; } = null!;
	}
}
