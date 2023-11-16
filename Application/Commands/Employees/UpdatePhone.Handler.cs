using Application.Services.Infrastructure;
using Domain.Aggregates;
using Domain.Values;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Employees
{
    class UpdatePhoneHandler : IRequestHandler<UpdatePhone, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<int, Employee> _retrieval;
        private readonly ILogger<UpdatePhoneHandler> _logger;

        public UpdatePhoneHandler(
            IEmployeePersistence persistence,
            IEntityRetrieval<int, Employee> retrieval,
            ILogger<UpdatePhoneHandler> logger)
        {
            _persistence = persistence;
            _retrieval = retrieval;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePhone request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.Payload.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.UpdatePhone(new PhoneNumber(phone: request.Payload.PhoneNumber.Phone, mobilePhone: request.Payload.PhoneNumber.MobilePhone));

            await _persistence.UpdatePhone(modification);

            _logger.LogInformation("Employee with Id: {id} change phone to : {@phone}", modification.Id, modification.PhoneNumber);

            return Unit.Value;
        }
    }
}
