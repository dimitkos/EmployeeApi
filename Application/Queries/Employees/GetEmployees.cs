using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    public class GetEmployees : IRequest<EmployeeModel[]>
    {
        public long[] EmployeeIds { get; }

        public GetEmployees(long[] employeeIds)
        {
            EmployeeIds = employeeIds;
        }
    }
}
