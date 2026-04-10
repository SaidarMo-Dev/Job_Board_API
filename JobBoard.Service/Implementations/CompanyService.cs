using JobBoard.Data.Entities;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class CompanyService : ICompanyService
	{
		#region Fields
		private readonly ICompanyRepository _companyRepository;
		private readonly ICurrentUserService _currentUserService;
		#endregion

		#region Constructors
		public CompanyService(ICompanyRepository companyRepository,
			ICurrentUserService currentUserService)
		{
			_companyRepository = companyRepository;
			_currentUserService = currentUserService;
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

		public IQueryable<Company> GetCompaniesQueryable()
		{
			var queryable = _companyRepository.GetTableAsNoTracking();

			return queryable;

		}

		public async Task<string[]> GetPopularCompaniesAsync()
		{

			var cutOffDate = DateTime.UtcNow.AddDays(-30);

			var popularCompanies = await _companyRepository
				.GetTableAsNoTracking()
				.Select(c => new
				{
					c.CompanyName,
					JobsCount =
						c.JobListings.Count(j => j.Status == JobStatusEnum.Active && j.DatePosted >= cutOffDate)

				})
				.OrderByDescending(c => c.JobsCount)
				.Select(c => c.CompanyName)
				.ToArrayAsync();

			return popularCompanies;


		}

		public async Task<bool> IsCreatedByUserAsync(int companyId, int userId)

			=> await _companyRepository.GetTableAsNoTracking()
					.AnyAsync(c => c.CompanyId == companyId && c.CreatedByUserId == userId);

		public async Task<Company> GetCompanyBySlugAsync(string slug)
		{
			var result = await _companyRepository.GetTableAsNoTracking()
				.FirstOrDefaultAsync(c => c.Slug == slug);

			return result;

		}

		public IQueryable<Company> GetFeaturedCompanies()
		{
			var result = _companyRepository.GetTableAsNoTracking().Where(c => c.IsFeatured);

			return result;
		}

		public async Task<bool> IsSlugExist(string slug)
		{

			var result = await _companyRepository.GetTableAsNoTracking().FirstOrDefaultAsync(c => c.Slug == slug);

			return result != null;
		}

		public async Task<bool> IsSlugExistExcludeSelf(string slug, int id)
		{
			var result = await _companyRepository.GetTableAsNoTracking().FirstOrDefaultAsync(c => c.Slug == slug && c.CompanyId != id);

			return result != null;
		}

		public async Task<Company> GetEmployerCompany()
		{
			var userId = _currentUserService.GetCurrentUserId();

			var company = await _companyRepository.GetTableAsNoTracking()
				.Where(c => c.CreatedByUserId == userId)
				.Include(x => x.CompanyIndustries).ThenInclude(x => x.Industry)
				.OrderByDescending(x => x.CreatedAt)
				.FirstOrDefaultAsync();

			return company;
		}

		#endregion
	}
}
