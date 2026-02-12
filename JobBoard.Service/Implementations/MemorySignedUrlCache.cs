using JobBoard.Service.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace JobBoard.Service.Implementations
{
	public class MemorySignedUrlCache : ISignedUrlCache
	{
		private readonly IMemoryCache _cache;

		public MemorySignedUrlCache(IMemoryCache cache)
		{
			_cache = cache;
		}

		public Task<string?> GetAsync(string key)
		{
			_cache.TryGetValue(key, out string? value);
			return Task.FromResult(value);
		}

		public Task SetAsync(string key, string value, TimeSpan duration)
		{
			var options = new MemoryCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = duration
			};

			_cache.Set(key, value, options);
			return Task.CompletedTask;
		}

		public Task RemoveAsync(string key)
		{
			_cache.Remove(key);
			return Task.CompletedTask;
		}
	}

}
