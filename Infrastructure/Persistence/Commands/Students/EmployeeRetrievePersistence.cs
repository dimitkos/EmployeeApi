using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Commands.Students
{
    class EmployeeRetrievePersistence : IEntityRetrieval<int, Employee>
    {
        private readonly DbContextOptions<EmployeeDbContext> _options;

        public EmployeeRetrievePersistence(DbContextOptions<EmployeeDbContext> options)
        {
            _options = options;
        }

        public async Task<Employee?> TryRetrieve(int key)
        {
            using var context = new EmployeeDbContext(_options);

            var employee = await context.Employees.FindAsync(key);

            return employee;
        }
    }
}
