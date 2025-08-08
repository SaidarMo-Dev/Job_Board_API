using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class CompanyService : ICompanyService
	{
		#region Fields
		private readonly ICompanyRepository _companyRepository;
		#endregion

		#region Constructors
		public CompanyService(ICompanyRepository companyRepository)
		{
			this._companyRepository = companyRepository;
		}

		#endregion

		#region Methods

		public async Task<ICollection<Company>> GetAllAsync()
		{
			return await _companyRepository.GetAllAsync();
		}
		public async Task<Company> GetCompanyByIdAsync(int Id) => await _companyRepository.FindByIdAsync(Id);

		public async Task AddAsync(Company entity) => await _companyRepository.AddAsync(entity);

		public async Task UpdateAsync(Company entity)
		{
			await _companyRepository.UpdateAsync(entity);
		}
		public async Task<bool> IsExistByNameAsync(string companyName)
		{
			var company = await _companyRepository.GetTableAsNoTracking()
								.FirstOrDefaultAsync(x => x.CompanyName == companyName);

			if (company == null) return false;
			return true;

		}

		public async Task<bool> IsExistByIdAsync(int id)
		{
			var company = await _companyRepository.GetTableAsNoTracking()
								.Where(x => x.CompanyId == id)
								.FirstOrDefaultAsync();

			if (company == null) return false;

			return true;

		}

		public async Task<bool> IsExistByNameExcludeSelfAsync(int Id, string companyName)
		{

			var company = await _companyRepository.GetTableAsNoTracking()
							.FirstOrDefaultAsync(x => x.CompanyName == companyName && x.CompanyId != Id);


			if (company == null) return false;
			return true;
		}

		public async Task DeleteAsync(Company entity)
		{
			await _companyRepository.DeleteAsync(entity);
		}

		public IQueryable<Company> GetPaginatedQueryable()
		{
			return _companyRepository.GetTableAsNoTracking().AsQueryable();
		}

		public IQueryable<Company> FilterPaginatedQueryable(OrderCompanyEnum order)
		{
			var queryable = _companyRepository.GetTableAsNoTracking().AsQueryable();

			switch (order)
			{
				case OrderCompanyEnum.OrderByID:
					queryable = queryable.OrderBy(x => x.CompanyId);
					break;
				case OrderCompanyEnum.OrderByName:
					queryable = queryable.OrderBy(x => x.CompanyName);
					break;
				case OrderCompanyEnum.OrderByLocation:
					queryable = queryable.OrderBy(x => x.Location);
					break;

				default:
					queryable.OrderBy(x => x.CompanyId);
					break;
			}

			return queryable;
		}

		public IQueryable<Company> GetCompaniesQueryable(string? search, SortCompany? sort)
		{
			var queryable = _companyRepository.GetTableAsNoTracking();

			if (search != null)
			{
				queryable = queryable.Where(x => x.CompanyName.Contains(search));
			}

			if (sort != null)
			{
				switch (sort)
				{
					case SortCompany.NameAsc:
						queryable = queryable.OrderBy(x => x.CompanyName);
						break;

					case SortCompany.NameDesc:
						queryable = queryable.OrderByDescending(x => x.CompanyName);
						break;

					default:
						queryable = queryable.OrderBy(x => x.CompanyName);
						break;
				}
			}

			return queryable;

		}
		#endregion
	}
}
