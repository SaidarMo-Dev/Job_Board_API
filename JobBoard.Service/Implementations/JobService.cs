using JobBoard.Data.Entities;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class JobService : IJobService
	{

		#region Fields
		private readonly IJobRepository _jobRepository;
		private readonly ISkillService _skillService;
		private readonly ICategoryService _categoryService;
		#endregion


		#region Constructors
		public JobService(IJobRepository jobRepository,
						ISkillService skillService,
						ICategoryService categoryService)
		{
			_jobRepository = jobRepository;
			_skillService = skillService;
			_categoryService = categoryService;
		}
		#endregion


		#region Methods
		public async Task<string> AddNewJobAsync(JobListing entity)
		{
			await _jobRepository.AddAsync(entity);
			return "Success";
		}

		public async Task<JobListing> GetJobByIdWithEncludeAsync(int Id)
		{
			var result = await _jobRepository.GetTableAsNoTracking()
					.Include(x => x.company)
					.Include(x => x.CreatedByUser)
					.Include(x => x.jobCategories).ThenInclude(x => x.category)
					.Include(x => x.JobSkills).ThenInclude(x => x.skillInfo)
					.FirstOrDefaultAsync(x => x.JobId == Id);

			return result;
		}

		public IQueryable<JobListing> GetJobsQueryable()
		{
			return _jobRepository.GetTableAsNoTracking().AsQueryable().IgnoreQueryFilters();
		}


		public async Task<List<Category>> GetJobCategoriesAsync(int JobId)
		{
			return await _categoryService.GetJobCategories(JobId).ToListAsync();
		}

		public async Task<List<Skill>> GetJobSkillsAsync(int JobId)
		{
			return await _skillService.GetJobSkills(JobId).ToListAsync();
		}

		public async Task<JobListing> GetJobByIdAsync(int Id)
		{
			var result = await _jobRepository.GetTableAsNoTracking()
								.Where(x => x.JobId.Equals(Id))
								.FirstOrDefaultAsync();

			return result;
		}

		public async Task<JobListing> UpdateAsync(JobListing job)
		{
			await _jobRepository.UpdateAsync(job);
			return job;
		}

		public async Task<bool> DeleteJobAsync(JobListing job)
		{
			var trans = _jobRepository.BeginTransaction();
			try
			{
				await _jobRepository.DeleteAsync(job);
				await trans.CommitAsync();
				return true;
			}
			catch (Exception ex)
			{
				await trans.RollbackAsync();
				return false;
			}
		}

		public async Task<bool> IsExistByIdAsync(int JobId)
		{
			var job = await _jobRepository.GetTableAsNoTracking()
									.Where(x => x.JobId.Equals(JobId))
									.FirstOrDefaultAsync();
			return job != null;
		}

		public async Task<JobListing> GetJobByIdWithEncludeSkillsAndCategoriesAsync(int Id)
		{
			var result = await _jobRepository.GetTableAsNoTracking()
								.Include(x => x.JobSkills)
								.Include(x => x.jobCategories)
								.Where(x => x.JobId.Equals(Id))
								.FirstOrDefaultAsync();

			return result;
		}

		public async Task<List<JobListing>> GetJobsByCompanyIdAsync(int CompanyId)
		{
			return await _jobRepository.GetTableAsNoTracking()
							.Where(x => x.CompanyId.Equals(CompanyId)).ToListAsync();

		}

		#endregion

	}
}
