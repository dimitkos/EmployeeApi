using Domain.Aggregates;

namespace Infrastructure.Cache
{
    class EmployeeCachingProvider : ICachingProvider<long, Employee>
    {
        private readonly ICacheAdapter<long, Employee> _cacheAdapter;

        public EmployeeCachingProvider(ICacheAdapter<long, Employee> cacheAdapter)
        {
            _cacheAdapter = cacheAdapter;
        }

        public void Remove(long[] keys)
            => _cacheAdapter.Remove(keys);

        public void Set(Employee employee)
            => _cacheAdapter.Set(employee.Id, employee);

    }
}
