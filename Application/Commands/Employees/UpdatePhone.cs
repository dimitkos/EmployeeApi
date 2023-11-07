using MediatR;
using Shared;

namespace Application.Commands.Employees
{
    public class UpdatePhone : IRequest<Unit>
    {
        public UpdatePhonePayload Payload { get; }

        public UpdatePhone(UpdatePhonePayload payload)
        {
            Payload = payload;
        }
    }
}
