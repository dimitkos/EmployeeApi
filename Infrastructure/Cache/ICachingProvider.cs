using Domain;

namespace Infrastructure.Cache
{
    interface ICachingProvider<TKey, TEntity> 
        where TEntity : class 
    {
        void Set(TEntity data);
        void Remove(TKey[] keys);
    }
}
