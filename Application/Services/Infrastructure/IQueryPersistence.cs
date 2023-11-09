namespace Application.Services.Infrastructure
{
    public interface IQueryPersistence<TRequest, TResult>
    {
        Task<TResult> Fetch(TRequest query);
    }
}
