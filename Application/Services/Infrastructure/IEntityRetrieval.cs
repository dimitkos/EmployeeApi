namespace Application.Services.Infrastructure
{
#warning check this
    public interface IEntityRetrieval<TKey, TResult> where TResult : class //and root entitykey
    {
        Task<TResult?> TryRetrieve(TKey key);
        Task<TResult> Retrieve(TKey key);
    }
}
