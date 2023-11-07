using MediatR;

namespace Application.Commands.Employees
{
    public class Promote : IRequest<Unit>
    {
        public int EmployeeId { get; }

        public Promote(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
