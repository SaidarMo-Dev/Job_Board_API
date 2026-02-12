namespace JobBoard.Service.Abstractions
{
	public interface ISignedUrlCache
	{
		Task<string?> GetAsync(string key);
		Task SetAsync(string key, string value, TimeSpan duration);
		Task RemoveAsync(string key);
	}

}
