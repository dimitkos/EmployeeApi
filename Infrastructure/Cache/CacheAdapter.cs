using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache
{
    interface ICacheAdapter<TKey, TEntity> where TKey : notnull
    {
        void Set(TKey key, TEntity value);
        void Remove(TKey[] keys);

        TEntity? TryGet(TKey key);
        Dictionary<TKey, TEntity> GetMany(TKey[] keys);
    }

    class CacheAdapter<TKey, TEntity> : ICacheAdapter<TKey, TEntity> where TKey : notnull
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CacheAdapter(IMemoryCache cache)
        {
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) };
        }

        public TEntity? TryGet(TKey key)
        {
            if (_cache.TryGetValue(ToCacheKey(key), out var value))
                return (TEntity)value;

            return default;
        }

        public Dictionary<TKey, TEntity> GetMany(TKey[] keys)
        {
            var cacheEntries = new Dictionary<TKey, TEntity>();

            foreach (var key in keys)
            {
                if (!_cache.TryGetValue(ToCacheKey(key), out var value))
                    continue;

                cacheEntries[key] = (TEntity)value;
            }

            return cacheEntries;
        }

        public void Remove(TKey[] keys)
        {
            foreach (var key in keys)
                _cache.Remove(ToCacheKey(key));
        }

        public void Set(TKey key, TEntity value)
            => _cache.Set(ToCacheKey(key), value, _cacheOptions);

        private string ToCacheKey(TKey key)
            => $"{nameof(TKey)}-{key.ToString()}";
    }
}
