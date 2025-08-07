namespace JobBoard.Core.Feutures.Categories.Queries.Results
{
	public class GetSingleCategoryQueryResponse
	{
		public int CategoryId { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public string? CreateDate { get; set; }

	}
}
