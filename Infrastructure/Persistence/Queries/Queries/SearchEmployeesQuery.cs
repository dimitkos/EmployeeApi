using Application.Queries.Employees;
using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Persistence.Queries.Queries
{
    class SearchEmployeesQuery : IQueryPersistence<SearchEmployees, EmployeeModel[]>
    {
        private readonly DbContextOptions<EmployeeDbContext> _options;

        public SearchEmployeesQuery(DbContextOptions<EmployeeDbContext> options)
        {
            _options = options;
        }

        public async Task<EmployeeModel[]> Fetch(SearchEmployees query)
        {
            using var context = new EmployeeDbContext(_options);

            var employees = context.Employees as IQueryable<Employee>;

            if (query.Payload.Gender.HasValue)
                employees = employees.Where(x => x.Gender == query.Payload.Gender);

            if (query.Payload.IsManager.HasValue)
                employees = employees.Where(x => x.IsManager == query.Payload.IsManager);

            if (query.Payload.DateFrom.HasValue)
                employees = employees.Where(x => x.CreatedAt >= query.Payload.DateFrom);

            if (query.Payload.DateTo.HasValue)
                employees = employees.Where(x => x.CreatedAt <= query.Payload.DateTo);

            if (query.Payload.Salary.HasValue)
                employees = employees.Where(x => x.Salary >= query.Payload.Salary);

            if (query.Payload.Salary.HasValue)
                employees = employees.Where(x => x.Salary <= query.Payload.Salary);

            if (query.Payload.Email is not null)
                employees = employees.Where(x => x.Email.Contains(query.Payload.Email));

            if (query.Payload.Surname is not null)
                employees = employees.Where(x => x.Surname.Contains(query.Payload.Surname));

            if (query.Payload.Paging is not null)
                employees = employees.OrderBy(x => x.Id).Skip(query.Payload.Paging.Skip).Take(query.Payload.Paging.Take);

            var employeeModels = await employees.ToArrayAsync();

            return employeeModels
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
