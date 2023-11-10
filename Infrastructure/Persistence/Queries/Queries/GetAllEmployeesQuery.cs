using Application.Queries.Employees;
using Application.Services.Infrastructure;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Persistence.Queries.Queries
{
    class GetAllEmployeesQuery : IQueryPersistence<GetAllEmployees, EmployeeModel[]>
    {
        private readonly DbContextOptions<EmployeeDbContext> _options;

        public GetAllEmployeesQuery(DbContextOptions<EmployeeDbContext> options)
        {
            _options = options;
        }

        public async Task<EmployeeModel[]> Fetch(GetAllEmployees query)
        {
            using var context = new EmployeeDbContext(_options);

            var employees = await context.Employees
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
