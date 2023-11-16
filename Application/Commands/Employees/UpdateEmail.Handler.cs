using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Employees
{
    class UpdateEmailHandler : IRequestHandler<UpdateEmail, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<int, Employee> _retrieval;
        private readonly ILogger<UpdateEmailHandler> _logger;

        public UpdateEmailHandler(
            IEmployeePersistence persistence,
            IEntityRetrieval<int, Employee> retrieval,
            ILogger<UpdateEmailHandler> logger)
        {
            _persistence = persistence;
            _retrieval = retrieval;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateEmail request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.Payload.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.UpdateEmail(request.Payload.Email);

            await _persistence.UpdateEmail(modification);

            _logger.LogInformation("Employee with Id: {id} change email to : {email}", modification.Id, modification.Email);

            return Unit.Value;
        }
    }
}
