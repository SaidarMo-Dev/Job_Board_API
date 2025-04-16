using JobBoard.Data.Entities.Identity;

namespace JobBoard.Data.Entities
{
	public class Country
	{
		public int CountryId { get; set; }
		public string CountryName { get; set; } = string.Empty;

		public ICollection<User> Users { get; set; } = new List<User>();
	}


}


