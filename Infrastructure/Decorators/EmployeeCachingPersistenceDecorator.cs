using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Cache;

namespace Infrastructure.Decorators
{
    class EmployeeCachingPersistenceDecorator : IEmployeePersistence
    {
        private readonly IEmployeePersistence _persistence;
        private readonly ICachingProvider<int, Employee> _cachingProvider;

        public EmployeeCachingPersistenceDecorator(IEmployeePersistence persistence, ICachingProvider<int, Employee> cachingProvider)
        {
            _persistence = persistence;
            _cachingProvider = cachingProvider;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _persistence.AddEmployee(employee);

            _cachingProvider.Set(employee);
        }

        public async Task DeleteEmployees(int[] employeeIds)
        {
            await _persistence.DeleteEmployees(employeeIds);

            _cachingProvider.Remove(employeeIds);
        }

        public async Task Promote(Employee employee)
        {
            await _persistence.Promote(employee);

            _cachingProvider.Set(employee);
        }

        public async Task UpdateEmail(Employee employee)
        {
            await _persistence.UpdateEmail(employee);

            _cachingProvider.Set(employee);
        }

        public async Task UpdatePhone(Employee employee)
        {
            await _persistence.UpdatePhone(employee);

            _cachingProvider.Set(employee);
        }

        public async Task UpdateSalary(Employee employee)
        {
            await _persistence.UpdateSalary(employee);

            _cachingProvider.Set(employee);
        }
    }
}
