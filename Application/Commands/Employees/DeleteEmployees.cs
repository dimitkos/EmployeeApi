using MediatR;

namespace Application.Commands.Employees
{
    public class DeleteEmployees : IRequest<Unit>
    {
        public long[] EmployeeIds { get; }

        public DeleteEmployees(long[] employeeIds)
        {
            EmployeeIds = employeeIds;
        }
    }
}
