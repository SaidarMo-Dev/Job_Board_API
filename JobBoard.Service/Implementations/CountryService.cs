using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class CountryService : ICountryService
	{

		#region Fields
		private readonly ICountryRepository _countryRepository;
		#endregion

		#region Constructors
		public CountryService(ICountryRepository countryRepository)
		{
			_countryRepository = countryRepository;
		}

		#endregion

		#region Methods
		public async Task<ICollection<Country>> GetAllAsync()
		{
			return await _countryRepository.GetAllAsync();
		}

		public Task<Country> GetCountryByIdAsync(int Id)
		{
			return _countryRepository.GetCountryByIdAsyn(Id);
		}

		public async Task<int> GetCountryIdAsync(string countryName)
		{
			return _countryRepository.GetTableAsNoTracking().Where(c => c.CountryName == countryName)
							.Select(x => x.CountryId)
							.FirstOrDefault();
		}

		public async Task<bool> IsCountryExist(string CountryName)
		{
			var country = await _countryRepository.GetTableAsNoTracking()
							.FirstOrDefaultAsync(x => x.CountryName == CountryName);

			if (country == null) return false;
			return true;

		}
		#endregion


	}

}
