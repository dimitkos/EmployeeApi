using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;

namespace Application.Commands.Employees
{
    class UpdateEmailHandler : IRequestHandler<UpdateEmail, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<int, Employee> _retrieval;

        public UpdateEmailHandler(IEmployeePersistence persistence, IEntityRetrieval<int, Employee> retrieval)
        {
            _persistence = persistence;
            _retrieval = retrieval;
        }

        public async Task<Unit> Handle(UpdateEmail request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.Payload.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.UpdateEmail(request.Payload.Email);

            await _persistence.UpdateEmail(modification);

            return Unit.Value;
        }
    }
}
