using Application.Services.Infrastructure;
using Domain.Aggregates;
using Domain.Values;
using MediatR;

namespace Application.Commands.Employees
{
    class UpdatePhoneHandler : IRequestHandler<UpdatePhone, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<Employee, int> _retrieval;

        public UpdatePhoneHandler(IEmployeePersistence persistence, IEntityRetrieval<Employee, int> retrieval)
        {
            _persistence = persistence;
            _retrieval = retrieval;
        }

        public async Task<Unit> Handle(UpdatePhone request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.Payload.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.UpdatePhone(new PhoneNumber(phone: request.Payload.PhoneNumber.Phone, mobilePhone: request.Payload.PhoneNumber.MobilePhone));

            await _persistence.UpdatePhone(modification);

            return Unit.Value;
        }
    }
}
