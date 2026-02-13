namespace JobBoard.Core.Feutures.Jobs.Queries.Responses
{
	public class CompanyPreviewDto
	{
		public int CompanyId { get; set; }
		public string Name { get; set; } = default!;
		public string? LogoUrl { get; set; }
	}

}
