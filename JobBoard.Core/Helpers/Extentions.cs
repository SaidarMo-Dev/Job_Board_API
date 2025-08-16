using System.Linq.Expressions;
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
				SortEnum.LowestSalary => queryable.OrderBy(x => x.MinSalary),
				_ => queryable.OrderByDescending(x => x.DatePosted),
			};


			return queryable;
		}

		public static IQueryable<JobListing> ApplySearch(this IQueryable<JobListing> queryable, string? title = null, string? location = null)
		{
			if (title != null)
				queryable = queryable.Where(x => x.Title.Contains(title));
			if (location != null)
				queryable = queryable.Where(x => x.Location.Contains(location));

			return queryable;
		}
		public static IQueryable<Application> FilterUserApplications(this IQueryable<Application> source, ApplicationStatusFilter filter)
		{
			switch (filter)
			{
				case ApplicationStatusFilter.Pending:
					source = source.Where(x => x.status == ApplicationStatusEnum.Pending);
					break;
				case ApplicationStatusFilter.Rejected:
					source = source.Where(x => x.status.Equals(ApplicationStatusEnum.Rejected));
					break;
				case ApplicationStatusFilter.Accepted:
					source = source.Where(x => x.status.Equals(ApplicationStatusEnum.Accepted));
					break;
				case ApplicationStatusFilter.All:
				default:
					break;

			}

			return source;
		}
		public static IQueryable<JobListing> FilterJobs(this IQueryable<JobListing> source, JobStatusEnum[]? status = null, string[]? categories = null, string[]? locations = null, string[]? companies = null, DateTime? from = null, DateTime? to = null)
		{
			source = source.AddFilter(status, x => x.Status);
			source = source.AddFilter(locations, x => x.Location);
			source = source.AddFilter(companies, x => x.company.CompanyName);

			// handle categories 
			if (categories is { Length: > 0 })
				source = source.Where(x => x.jobCategories
					.Any(jc => categories.Contains(jc.category.Name)));


			if (from is not null)
				source = source.Where(x => x.DatePosted >= from);

			if (to is not null)
				source = source.Where(x => x.DateExpired <= to);

			return source;
		}

		public static IQueryable<T> AddFilter<T, TValue>(
	  this IQueryable<T> source,
	  TValue[]? values,
	  Expression<Func<T, TValue>> selector)
		{
			// If null or empty, do nothing
			if (values is not { Length: > 0 })
				return source;

			// Builds: x => values.Contains(selector(x))
			var parameter = selector.Parameters[0];
			var property = selector.Body;
			var valuesConstant = Expression.Constant(values);

			var containsCall = Expression.Call(
				typeof(Enumerable),
				nameof(Enumerable.Contains),
				new[] { typeof(TValue) },
				valuesConstant,
				property
			);

			var lambda = Expression.Lambda<Func<T, bool>>(containsCall, parameter);
			return source.Where(lambda);
		}
	}

}
