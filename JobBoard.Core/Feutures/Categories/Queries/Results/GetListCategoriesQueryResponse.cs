namespace JobBoard.Core.Feutures.Categories.Queries.Results
{
	public class GetListCategoriesQueryResponse
	{
		public int CategoryId { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public required string CreateDate { get; set; }
	}
}
