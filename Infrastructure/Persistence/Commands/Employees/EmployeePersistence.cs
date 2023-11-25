using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Commands.Employees
{
    class EmployeePersistence : IEmployeePersistence
    {
        private readonly DbContextOptions<EmployeeDbContext> _options;

        public EmployeePersistence(DbContextOptions<EmployeeDbContext> options)
        {
            _options = options;
        }

        public async Task AddEmployee(Employee employee)
        {
            using var context = new EmployeeDbContext(_options);

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }

        public async Task Promote(Employee employee)
        {
            using var context = new EmployeeDbContext(_options);

            var employeeEntry = context.Entry(employee);
            employeeEntry.Property(x => x.IsManager).IsModified = true;
            employeeEntry.Property(x => x.UpdatedAt).IsModified = true;

            await context.SaveChangesAsync();
        }

        public async Task UpdateEmail(Employee employee)
        {
            using var context = new EmployeeDbContext(_options);

            var employeeEntry = context.Entry(employee);
            employeeEntry.Property(x => x.Email).IsModified = true;
            employeeEntry.Property(x => x.UpdatedAt).IsModified = true;

            await context.SaveChangesAsync();
        }

        public async Task UpdatePhone(Employee employee)
        {
            using var context = new EmployeeDbContext(_options);

            var employeeEntry = context.Attach(employee);
            var phoneEntry = employeeEntry.Reference(e => e.PhoneNumber);
            phoneEntry.TargetEntry!.Property(e => e.Phone).IsModified = true;
            phoneEntry.TargetEntry.Property(e => e.MobilePhone).IsModified = true;
            employeeEntry.Property(x => x.UpdatedAt).IsModified = true;

            await context.SaveChangesAsync();
        }

        public async Task UpdateSalary(Employee employee)
        {
            using var context = new EmployeeDbContext(_options);

            var employeeEntry = context.Entry(employee);
            employeeEntry.Property(x => x.Salary).IsModified = true;
            employeeEntry.Property(x => x.UpdatedAt).IsModified = true;

            await context.SaveChangesAsync();
        }

        public async Task DeleteEmployees(long[] employeeIds)
        {
            using var context = new EmployeeDbContext(_options);

            await context.Database.ExecuteSqlRawAsync($"Delete from Employees where Id in ({string.Join(',', employeeIds)})");
        }
    }
}
