namespace JobBoard.Data.Entities
{
	public class Industry
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Slug { get; set; } = default!;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


		public ICollection<CompanyIndustry> CompanyIndustries { get; set; } = new List<CompanyIndustry>();
	}

}
