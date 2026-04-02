using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class UserLanguage
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Language { get; set; } = null!;
		public string Proficiency { get; set; } = null!;

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public User User { get; set; } = null!;
	}
}
