using AutoMapper;

namespace JobBoard.Core.Mapping.CountryMapping
{
	public partial class CountryProfile : Profile
	{
		public CountryProfile()
		{
			ListCountryResponseMapping();
		}
	}
}
