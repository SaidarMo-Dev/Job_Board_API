using System.Linq.Expressions;
using JobBoard.Data.Entities;
using JobBoard.Data.enums;

namespace JobBoard.Core.Helpers
{
	public static class Extention
	{
		public static IQueryable<JobListing> ApplyFilters(
					this IQueryable<JobListing> source,
					JobTypeEnum[]? jobTypes = null,
					ExperienceLevelEnum[]? experienceLevels = null,
					SortEnum? sortBy = null,
					string[]? companies = null,
					string[]? categories = null)
		{
			// handle filter by companies
			source = source.AddFilter(companies, x => x.company.CompanyName);
			// handle filter by job types

			if (jobTypes != null && !jobTypes.Contains(JobTypeEnum.Any))
				source = source.AddFilter(jobTypes, x => x.JobType);

			// handle filter by experiences

			if (experienceLevels != null && !experienceLevels.Contains(ExperienceLevelEnum.Any))
				source = source.AddFilter(experienceLevels, x => x.ExperienceLevel);

			// handle categories 
			if (categories is { Length: > 0 })
			{
				var categorySet = categories.ToHashSet();
				source = source.Where(x => x.jobCategories
					.Any(jc => categorySet.Contains(jc.category.Name)));

			}


			source = sortBy switch
			{
				SortEnum.Recent => source.OrderByDescending(x => x.DatePosted),
				SortEnum.HighestSalary => source.OrderByDescending(x => x.MaxSalary),
				SortEnum.LowestSalary => source.OrderBy(x => x.MinSalary),
				_ => source.OrderByDescending(x => x.DatePosted),
			};


			return source;
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

		public static IEnumerable<TEnum> SafeParseEnums<TEnum>(this IEnumerable<string>? values)
			where TEnum : struct, Enum
		{
			if (values == null) yield break;

			foreach (var value in values)
			{
				if (Enum.TryParse<TEnum>(value, true, out var parsed))
					yield return parsed;
			}
		}

	}

}
