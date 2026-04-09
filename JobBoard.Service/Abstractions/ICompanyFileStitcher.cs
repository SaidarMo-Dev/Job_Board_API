namespace JobBoard.Service.Abstractions
{
	public interface ICompanyFileStitcher
	{
		Task AttachLogosAsync<T>(
			IEnumerable<T> items,
			Func<T, int> idSelector,
			Action<T, string?> urlSetter,
			CancellationToken ct = default);

		Task AttachLogosAndBannersAsync<T>(
			IEnumerable<T> items,
			Func<T, int> idSelector,
			Action<T, string?> urlLogoSetter,
			Action<T, string?> urlBannerSetter,
			CancellationToken ct = default);

		Task AttachLogoAndBannerAsync<T>(
		T company,
		int Id,
		Action<T, string?> urlLogoSetter,
		Action<T, string?> urlBannerSetter,
		CancellationToken ct = default);
	}

}
