using MediatR;

namespace Application.Commands.Employees
{
    public class Promote : IRequest<Unit>
    {
        public long EmployeeId { get; }

        public Promote(long employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
