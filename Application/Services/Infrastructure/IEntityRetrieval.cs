using Domain;

namespace Application.Services.Infrastructure
{
    public interface IEntityRetrieval<TKey, TResult> 
        where TResult : class 
    {
        Task<TResult?> TryRetrieve(TKey key);
        Task<TResult> Retrieve(TKey key);
    }
}
