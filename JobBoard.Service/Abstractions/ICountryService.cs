using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ICountryService
	{
		Task<Country> GetCountryByIdAsync(int Id);
		Task<ICollection<Country>> GetAllAsync();
		Task<int> GetIdByNameAsync(string countryName);
		Task<bool> IsExistByName(string CountryName);

	}

}
