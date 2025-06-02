using JobBoard.Core.Feutures.Countries.Queries.Responses;
using JobBoard.Data.Entities;

namespace JobBoard.Core.Mapping.CountryMapping
{
	public partial class CountryProfile
	{
		public void ListCountryResponseMapping()
		{
			CreateMap<Country, ListCountriesQueryResponse>();
		}
	}
}
