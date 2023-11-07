using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    public class GetEmployees : IRequest<EmployeeModel[]>
    {
        public int[] EmployeeIds { get; }

        public GetEmployees(int[] employeeIds)
        {
            EmployeeIds = employeeIds;
        }
    }
}
