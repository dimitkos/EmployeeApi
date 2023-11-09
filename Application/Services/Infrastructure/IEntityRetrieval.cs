namespace Application.Services.Infrastructure
{
#warning check this
    public interface IEntityRetrieval<TResult, K> where TResult : class //and root entitykey
    {
        Task<TResult?> TryRetrieve(K key);
    }
}
