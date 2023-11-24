namespace Infrastructure.Cache
{
    interface ICachingProvider<TKey, TEntity> where TEntity : class where TKey : notnull
    {
        void Set(TEntity data);
        void Remove(TKey[] keys);
    }
}
