using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace _4TeamTask
{
	public static class RedisCache
	{
		public static async Task SetRecordAsync<T>(this IDistributedCache cache,
			string recordID,
			T data,
			TimeSpan? absoluteExpireTime = null,
			TimeSpan? unusedExpiredTime = null)
		{
			var options = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(30),
				SlidingExpiration = unusedExpiredTime
			};

			var jsonData = JsonSerializer.Serialize(data);

			await cache.SetStringAsync(recordID, jsonData, options);
		}

		public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordID)
		{
			var jsonData = await cache.GetStringAsync(recordID);

			if (jsonData is null)
			{
				return default(T);
			}

			return JsonSerializer.Deserialize<T>(jsonData);
		}
	}
}