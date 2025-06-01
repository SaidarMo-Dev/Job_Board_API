namespace JobBoard.Core.Feutures.Countries.Queries.Responses
{
	public class GetCountryByIdQueryResponse
	{
		public int Id { get; set; }
		public required string CountryName { get; set; }
	}
}
