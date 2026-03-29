namespace JobBoard.Data.Entities
{
	public class CompanyIndustry
	{
		public int CompanyId { get; set; }
		public int IndustryId { get; set; }

		public Company Company { get; set; } = default!;
		public Industry Industry { get; set; } = default!;
	}

}
