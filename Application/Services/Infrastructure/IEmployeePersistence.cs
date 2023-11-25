using Domain.Aggregates;

namespace Application.Services.Infrastructure
{
    public interface IEmployeePersistence
    {
        Task AddEmployee(Employee employee);
        Task Promote(Employee employee);
        Task UpdateEmail(Employee employee);
        Task UpdatePhone(Employee employee);
        Task UpdateSalary(Employee employee);
        Task DeleteEmployees(long[] employeeIds);
    }
}
