namespace Application.Services.Infrastructure
{
#warning check this
    public interface IEntityRetrieval<T,K> where T : class //and root entitykey
    {
        Task<T?> TryRetrieve(K key);
    }
}
