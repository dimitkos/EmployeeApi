using MediatR;

namespace Application.Commands.Employees
{
    public class DeleteEmployees : IRequest<Unit>
    {
        public int[] EmployeeIds { get; }

        public DeleteEmployees(int[] employeeIds)
        {
            EmployeeIds = employeeIds;
        }
    }
}
