using JobBoard.Data.Entities;
using JobBoard.Data.Entities.Identity;
using JobBoard.Data.enums;
using JobBoard.Infrastructure.Abstractions;
using JobBoard.Service.Abstractions;
using JobBoard.Service.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class JobService : IJobService
	{

		#region Fields
		private readonly IJobRepository _jobRepository;
		private readonly ISkillService _skillService;
		private readonly ICategoryService _categoryService;
		private readonly ICurrentUserService _currentUserService;
		#endregion


		#region Constructors
		public JobService(IJobRepository jobRepository,
						ISkillService skillService,
						ICategoryService categoryService,
						ICurrentUserService currentUserService)
		{
			_jobRepository = jobRepository;
			_skillService = skillService;
			_categoryService = categoryService;
			_currentUserService = currentUserService;
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
					.Include(x => x.Company)
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

		public async Task<string[]> GetPopularLocations()
		{
			var cutOffDate = DateTime.UtcNow.AddDays(-30);

			return (await _jobRepository.GetTableAsNoTracking()
				.Where(x => x.Status == Data.enums.JobStatusEnum.Active && x.DatePosted >= cutOffDate)
				.GroupBy(x => x.Location)
				.Select(g => new
				{
					location = g.Key,
					jobCount = g.Count()
				})
				.OrderByDescending(x => x.jobCount).
				Take(10)
				.Select(x => x.location)
				.ToArrayAsync());

		}

		public IQueryable<JobListing> GetRecommendationJobs(User user, int take = 3)
		{

			var queryableJobs = _jobRepository.GetTableAsNoTracking();

			var InteractedJobIds = queryableJobs
									.Where(j => j.bookMarks.Any(b => b.UserId == user.Id)
											|| j.applications.Any(ap => ap.UserId == user.Id))
									.Select(j => j.JobId).ToHashSet();


			var interactedCategories = queryableJobs
							.Where(j => InteractedJobIds.Contains(j.JobId))
							.SelectMany(j => j.jobCategories).Select(jc => jc.CategoryId)
							.Distinct().ToList();



			var recommendations = queryableJobs
				.Where(j => !InteractedJobIds.Contains(j.JobId)
						&& j.DateExpired > DateTime.Now
						&& j.Status == JobStatusEnum.Active)
				.Select(job =>
					new
					{
						Score =   // Location match
								(job.Location != null && user.Country != null && job.Location.Contains(user.Country.CountryName) ? 0.3 : 0) +

								// Category match
								(job.jobCategories.Any(x => interactedCategories.Contains(x.CategoryId)) ? 0.5 : 0) +

								// Recency bonus (using DateDiffDay for SQL translation)
								(1.0 / (EF.Functions.DateDiffDay(job.DatePosted, DateTime.UtcNow) + 1) * 0.1),

						Job = job

					})
				.Where(j => j.Score > 0)
				.OrderByDescending(x => x.Score)
				.ThenByDescending(x => x.Job.DatePosted)
				.Take(take)
				.Select(x => x.Job);


			return recommendations;
		}

		public IQueryable<JobListing> GetEmployerPostedJobsQueryable(int userId, string? search)
		{
			var query = _jobRepository.GetTableAsNoTracking().Where(j => j.CreatedByUserId == userId);

			if (search != null) query = query.Where(j => j.Title.Contains(search));

			return query;
		}



		#endregion

	}
}
