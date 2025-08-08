using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Service.Abstractions
{
	public interface ICompanyService
	{
		/// <summary>
		/// Retrieves a company by its ID asynchronously.
		/// </summary>
		/// <param name="Id">The ID of the company.</param>
		/// <returns>The <see cref="Company"/> entity if found; otherwise, null.</returns>
		Task<Company> GetCompanyByIdAsync(int Id);

		/// <summary>
		/// Retrieves all companies asynchronously.
		/// </summary>
		/// <returns>A collection of all <see cref="Company"/> entities.</returns>
		Task<ICollection<Company>> GetAllAsync();

		/// <summary>
		/// Retrieves a queryable collection of companies for pagination purposes.
		/// </summary>
		/// <returns>An <see cref="IQueryable{Company}"/> that supports pagination.</returns>
		IQueryable<Company> GetPaginatedQueryable();

		/// <summary>
		/// Filters and orders the paginated queryable list of companies based on the specified order.
		/// </summary>
		/// <param name="order">The ordering criteria defined in <see cref="OrderCompanyEnum"/>.</param>
		/// <returns>A filtered and ordered <see cref="IQueryable{Company}"/>.</returns>
		IQueryable<Company> FilterPaginatedQueryable(OrderCompanyEnum order);

		/// <summary>
		/// Adds a new company asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="Company"/> entity to add.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task AddAsync(Company entity);

		/// <summary>
		/// Updates an existing company asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="Company"/> entity to update.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task UpdateAsync(Company entity);

		/// <summary>
		/// Checks asynchronously if a company with the given name exists.
		/// </summary>
		/// <param name="companyName">The name of the company to check.</param>
		/// <returns><c>true</c> if a company with the specified name exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByNameAsync(string companyName);

		/// <summary>
		/// Checks asynchronously if a company name exists, excluding the specified company ID.
		/// Useful for validation during updates.
		/// </summary>
		/// <param name="Id">The ID of the company to exclude.</param>
		/// <param name="companyName">The company name to check for existence.</param>
		/// <returns><c>true</c> if the name exists in another company; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByNameExcludeSelfAsync(int Id, string companyName);

		/// <summary>
		/// Checks asynchronously if a company exists by its ID.
		/// </summary>
		/// <param name="id">The ID of the company.</param>
		/// <returns><c>true</c> if the company exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsExistByIdAsync(int id);

		/// <summary>
		/// Deletes a company asynchronously.
		/// </summary>
		/// <param name="entity">The <see cref="Company"/> entity to delete.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task DeleteAsync(Company entity);

		IQueryable<Company> GetCompaniesQueryable(string? search, SortCompany? sort);


	}
}
