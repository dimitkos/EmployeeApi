using Application.Services.Infrastructure;
using Infrastructure.Cache;

namespace Infrastructure.Decorators
{
    class EntityRetrievalCachingPersistenceDecorator<TKey, TResult> : IEntityRetrieval<TKey, TResult>
        where TKey : notnull
        where TResult : class
    {
        private readonly IEntityRetrieval<TKey, TResult> _retrieval;
        private readonly ICacheAdapter<TKey, TResult> _cache;

        public EntityRetrievalCachingPersistenceDecorator(IEntityRetrieval<TKey, TResult> retrieval, ICacheAdapter<TKey, TResult> cache)
        {
            _retrieval = retrieval;
            _cache = cache;
        }

        public async Task<TResult> Retrieve(TKey key)
        {
            var cacheItem = _cache.TryGet(key);

            if (cacheItem is not null)
                return (TResult)cacheItem;

            var entity = await _retrieval.Retrieve(key);

            _cache.Set(key, entity);

            return entity;
        }

        public async Task<TResult?> TryRetrieve(TKey key)
        {
            var cacheItem = _cache.TryGet(key);

            if (cacheItem is not null)
                return (TResult?)cacheItem;

            var entity = await _retrieval.TryRetrieve(key);

            if(entity is null)
                return entity;

            _cache.Set(key, entity);

            return entity;
        }
    }
}
