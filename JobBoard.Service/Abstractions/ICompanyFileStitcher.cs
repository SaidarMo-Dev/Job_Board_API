namespace JobBoard.Service.Abstractions
{
	/// <summary>
	/// Provides high-performance utility methods to decorate DTOs with company-related media URLs.
	/// </summary>

	public interface ICompanyFileStitcher
	{

		/// <summary>
		/// Asynchronously fetches and attaches media URLs (Logos/Banners) to a collection of items in a single database round-trip.
		/// </summary>
		/// <typeparam name="T">The type of the DTO to be decorated.</typeparam>
		/// <param name="items">The collection of items requiring media URLs.</param>
		/// <param name="idSelector">A function to extract the Company ID from each item.</param>
		/// <param name="logoSetter">An optional action to assign the resolved Logo URL to a property on the item.</param>
		/// <param name="bannerSetter">An optional action to assign the resolved Banner URL to a property on the item.</param>
		/// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous decoration operation.</returns>

		Task AttachFilesAsync<T>(
			IEnumerable<T> items,
			Func<T, int> idSelector,
			Action<T, string?>? logoSetter = null,
			Action<T, string?>? bannerSetter = null,
			CancellationToken ct = default);

		/// <summary>
		/// Asynchronously fetches and attaches media URLs (Logos/Banners) to a single item.
		/// </summary>
		/// <typeparam name="T">The type of the DTO to be decorated.</typeparam>
		/// <param name="item">The specific item requiring media URLs.</param>
		/// <param name="idSelector">A function to extract the Company ID from the item.</param>
		/// <param name="logoSetter">An optional action to assign the resolved Logo URL.</param>
		/// <param name="bannerSetter">An optional action to assign the resolved Banner URL.</param>
		/// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous decoration operation.</returns>
		Task AttachFilesAsync<T>(
			T item,
			Func<T, int> idSelector,
			Action<T, string?>? logoSetter = null,
			Action<T, string?>? bannerSetter = null,
			CancellationToken ct = default);

	}

}
