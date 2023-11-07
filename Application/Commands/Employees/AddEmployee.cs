using MediatR;
using Shared;

namespace Application.Commands.Employees
{
    public class AddEmployee : IRequest<Unit>
    {
        public AddEmployeePayload Payload { get; }

        public AddEmployee(AddEmployeePayload payload)
        {
            Payload = payload;
        }
    }
}
