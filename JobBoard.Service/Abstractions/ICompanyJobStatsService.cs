namespace JobBoard.Service.Abstractions
{
	public interface ICompanyJobStatsService
	{

		Task GetCompanyJobStatsAsync<T>(
			IEnumerable<T> items,
			Func<T, int> idSelector,
			Action<T, int>? jobSetter = null,
			Action<T, int>? openJobSetter = null,
			CancellationToken cancellationToken = default);

		Task GetCompanyJobStatsAsync<T>(
			T item,
			Func<T, int> idSelector,
			Action<T, int>? jobSetter = null,
			Action<T, int>? openJobSetter = null,
			CancellationToken cancellationToken = default);
	}
}
