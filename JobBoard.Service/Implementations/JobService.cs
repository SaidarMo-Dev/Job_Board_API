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
					.Include(x => x.UserInfo)
					.Include(x => x.jobCategories)
					.Include(x => x.Jobskills).ThenInclude(x => x.skillInfo)
					.FirstOrDefaultAsync(x => x.JobId == Id);

			return result;
		}

		public IQueryable<JobListing> GetJobsQueryable()
		{
			return _jobRepository.GetTableAsNoTracking().AsQueryable();
		}


		public async Task<List<Category>> GetJobCategoriesAsync(int JobId)
		{
			return await _categoryService.GetJobCategories(JobId).ToListAsync();
		}

		public async Task<List<Skill>> GetJobSkillsAsync(int JobId)
		{
			return await _skillService.GetJobSkills(JobId).ToListAsync();
		}

		#endregion

	}
}
