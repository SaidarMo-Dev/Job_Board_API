namespace JobBoard.Core.Feutures.Industry.Queries.Responses
{
	public class GetIndustriesQueryResponse
	{
		public int Id { get; set; }

		public string Name { get; set; } = default!;

		public string Slug { get; set; } = default!;
	}
}
