using Domain.Aggregates;

namespace Infrastructure.Cache
{
    class EmployeeCachingProvider : ICachingProvider<int, Employee>
    {
        private readonly ICacheAdapter<int, Employee> _cacheAdapter;

        public EmployeeCachingProvider(ICacheAdapter<int, Employee> cacheAdapter)
        {
            _cacheAdapter = cacheAdapter;
        }

        public void Remove(int[] keys)
            => _cacheAdapter.Remove(keys);

        public void Set(Employee employee)
            => _cacheAdapter.Set(employee.Id, employee);

    }
}
