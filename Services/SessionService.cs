using Microsoft.Extensions.Caching.Distributed;

namespace WebApplication1.Services
{
    public class SessionService
    {
        private readonly IDistributedCache _cache;

        public SessionService(IDistributedCache cache)
        {
            _cache = cache;
        }

        async public Task<string> CreateForUsername(string username, TimeSpan? ttl = default)
        {
            if (ttl == default)
            {
                ttl = TimeSpan.FromMinutes(2);
            }

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            };

            var guidSessionValue = Guid.NewGuid();

            await _cache.SetStringAsync(username, guidSessionValue.ToString(), cacheOptions);
   
            return NormalizeSessionToken(username, guidSessionValue);
        }

        async public Task<string> UpdateForUsername(string username, TimeSpan? ttl = default)
        {

            if (ttl == default)
            {
                ttl = TimeSpan.FromMinutes(2);
            }

            return await this.CreateForUsername(username, ttl);
        }

        public async Task<bool> SessionExistsForUsername(string username, string uuid)
        {
            var sessionId = await _cache.GetStringAsync(username);

            return String.Equals(uuid, sessionId, StringComparison.OrdinalIgnoreCase);
        }

        async public Task ReleaseSessionForUsername(string username)
        {
            await _cache.RemoveAsync(username);
        }

        static private string NormalizeSessionToken(string username, Guid uuid)
        {
            return $"{username} {uuid}";
        }
    }
}
