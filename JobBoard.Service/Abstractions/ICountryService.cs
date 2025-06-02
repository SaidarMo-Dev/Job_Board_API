using JobBoard.Data.Entities;

namespace JobBoard.Service.Abstractions
{
	public interface ICountryService
	{
		/// <summary>
		/// Retrieves a country entity by its unique identifier asynchronously.
		/// </summary>
		/// <param name="Id">The unique identifier of the country.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Country"/> entity if found; otherwise, null.</returns>
		Task<Country> GetCountryByIdAsync(int Id);

		/// <summary>
		/// Retrieves all countries asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a collection of all <see cref="Country"/> entities.</returns>
		Task<ICollection<Country>> GetAllAsync();

		/// <summary>
		/// Retrieves the unique identifier of a country by its name asynchronously.
		/// </summary>
		/// <param name="countryName">The name of the country.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the ID of the country if found; otherwise, a default value.</returns>
		Task<int> GetIdByNameAsync(string countryName);

		/// <summary>
		/// Checks if a country exists by its name asynchronously.
		/// </summary>
		/// <param name="CountryName">The name of the country to check.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <c>true</c> if the country exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByName(string CountryName);


	}

}
