using Application.Queries.Employees;
using Application.Services.Infrastructure;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Persistence.Queries.Queries
{
    class GetEmployeesQuery : IQueryPersistence<GetEmployees, EmployeeModel[]>
    {
        private readonly DbContextOptions<EmployeeDbContext> _options;

        public GetEmployeesQuery(DbContextOptions<EmployeeDbContext> options)
        {
            _options = options;
        }

        public async Task<EmployeeModel[]> Fetch(GetEmployees query)
        {
            using var context = new EmployeeDbContext(_options);

            var employees = await context.Employees
                .Where(employee => query.EmployeeIds.Contains(employee.Id))
                .ToArrayAsync();

            return employees
                .Select(employee => new EmployeeModel(
                    id: employee.Id,
                    name: employee.Name,
                    surname: employee.Surname,
                    gender: employee.Gender,
                    isManager: employee.IsManager,
                    salary: employee.Salary,
                    email: employee.Email,
                    phoneNumber: new PhoneNumberModel(employee.PhoneNumber.Phone, employee.PhoneNumber.MobilePhone)))
                .ToArray();
        }
    }
}
