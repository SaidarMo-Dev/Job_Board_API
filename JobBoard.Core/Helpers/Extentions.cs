using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Core.Helpers
{
	public static class Extention
	{
		public static IQueryable<JobListing> ApplyFilters(
					this IQueryable<JobListing> queryable,
					JobTypeEnum? jobType = null,
					double? minSalary = null,
					double? maxSalary = null,
					ExperienceLevelEnum? experienceLevel = null,
					SortEnum? sortBy = null)
		{
			if (jobType.HasValue)
				queryable = queryable.Where(x => x.JobType == jobType.Value);

			if (experienceLevel.HasValue)
				queryable = queryable.Where(x => x.ExperienceLevel == experienceLevel.Value);

			if (minSalary.HasValue)
				queryable = queryable.Where(x => x.MinSalary >= minSalary.Value);

			if (maxSalary.HasValue)
				queryable = queryable.Where(x => x.MaxSalary <= maxSalary.Value);


			queryable = sortBy switch
			{
				SortEnum.Recent => queryable.OrderByDescending(x => x.DatePosted),
				SortEnum.HighestSalary => queryable.OrderByDescending(x => x.MaxSalary),
				SortEnum.LowestSalary => queryable.OrderByDescending(x => x.MinSalary),
				_ => queryable.OrderByDescending(x => x.DatePosted),
			};


			return queryable;
		}

		public static IQueryable<JobListing> ApplySearch(this IQueryable<JobListing> queryable, string? title = null, string? location = null)
		{
			if (title != null)
				queryable = queryable.Where(x => x.Equals(title));
			if (location != null)
				queryable = queryable.Where(x => x.Location.Equals(location));

			return queryable;
		}
	}

}
