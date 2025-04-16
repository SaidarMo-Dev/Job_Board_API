using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ICountryService
	{
		Task<Country> GetCountryByIdAsync(int Id);
		Task<ICollection<Country>> GetAllAsync();
		Task<int> GetCountryIdAsync(string countryName);
		Task<bool> IsCountryExist(string CountryName);

	}

}
