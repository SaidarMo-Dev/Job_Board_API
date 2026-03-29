using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;

namespace JobBoard.Service.Implementations
{
	public class IndustryService : IIndustryService
	{

		#region Fields
		private readonly IIndustryRepository _industryRepository;

		#endregion

		#region Constructors
		public IndustryService(IIndustryRepository industryRepository)
		{
			_industryRepository = industryRepository;
		}
		#endregion


		#region Methods

		public async Task AddAsync(Industry entity)
		{
			await _industryRepository.AddAsync(entity);
		}

		public async Task DeleteAsync(Industry entity)
		{
			await _industryRepository.DeleteAsync(entity);
		}

		public IQueryable<Industry> GetIndustriesQueryable()
		{
			return _industryRepository.GetTableAsNoTracking().AsQueryable();
		}

		public async Task<Industry> GetByIdAsync(int id)
		{
			return await _industryRepository.FindByIdAsync(id);
		}

		public async Task UpdateAsync(Industry entity)
		{
			await _industryRepository.UpdateAsync(entity);
		}

		#endregion
	}
}
