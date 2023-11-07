using MediatR;
using Shared;

namespace Application.Commands.Employees
{
    public class UpdateSalary : IRequest<Unit>
    {
        public UpdateSalaryPayload Payload { get; }

        public UpdateSalary(UpdateSalaryPayload payload)
        {
            Payload = payload;
        }
    }
}
