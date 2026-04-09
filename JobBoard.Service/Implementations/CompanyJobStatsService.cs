using JobBoard.Service.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Service.Implementations
{
	public class CompanyJobStatsService : ICompanyJobStatsService
	{
		private readonly IJobService _jobService;

		public CompanyJobStatsService(IJobService jobService)
		{
			_jobService = jobService;
		}
		public async Task GetCompanyJobStatsAsync<T>(IEnumerable<T> items, Func<T, int> idSelector, Action<T, int>? jobSetter = null, Action<T, int>? openJobSetter = null, CancellationToken cancellationToken = default)
		{
			if (items == null) return;

			// Project the filtered companies to DTOs and apply pagination
			// This executes the query for the current page only

			var companyIds = items
			.Select(idSelector)
			.Distinct()
			.ToList();

			if (companyIds.Count == 0) return;

			// Current UTC time for calculating open jobs
			var now = DateTime.UtcNow;

			// Fetch job statistics for all companies in a single query
			// Group by company to get total jobs and open jobs
			var jobStats = await _jobService.GetJobsQueryable()
				.Where(j => companyIds.Contains(j.CompanyId))      // Only consider companies on the current page
				.GroupBy(j => j.CompanyId)
				.Select(g => new
				{
					CompanyId = g.Key,
					TotalJobs = g.Count(),                          // Total jobs per company
					OpenJobs = g.Count(j =>
						j.Status == Data.enums.JobStatusEnum.Active && // Only active jobs
						j.DateExpired > now)                            // Only jobs that haven't expired
				})
				.ToDictionaryAsync(x => x.CompanyId);              // Convert to dictionary for fast lookup

			foreach (var item in items)
			{
				if (jobStats.TryGetValue(idSelector(item), out var stats))
				{
					if (jobSetter != null)
						jobSetter(item, stats.TotalJobs);          // Assign total jobs

					if (openJobSetter != null)
						openJobSetter(item, stats.OpenJobs);       // Assign open jobs

				}

			}

		}

		public Task GetCompanyJobStatsAsync<T>(T item, Func<T, int> idSelector, Action<T, int>? jobSetter = null, Action<T, int>? openJobSetter = null, CancellationToken cancellationToken = default)
			=> GetCompanyJobStatsAsync([item], idSelector, jobSetter, openJobSetter, cancellationToken);


	}
}
