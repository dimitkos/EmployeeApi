using MediatR;
using Shared;

namespace Application.Commands.Employees
{
    public class UpdateEmail : IRequest<Unit>
    {
        public UpdateEmailPayload Payload { get; }

        public UpdateEmail(UpdateEmailPayload payload)
        {
            Payload = payload;
        }
    }
}
