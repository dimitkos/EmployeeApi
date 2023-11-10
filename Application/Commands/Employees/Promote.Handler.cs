using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;

namespace Application.Commands.Employees
{
    class PromoteHandler : IRequestHandler<Promote, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<int, Employee> _retrieval;

        public PromoteHandler(IEmployeePersistence persistence, IEntityRetrieval<int, Employee> retrieval)
        {
            _persistence = persistence;
            _retrieval = retrieval;
        }

        public async Task<Unit> Handle(Promote request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.Promote();

            await _persistence.Promote(modification);

            return Unit.Value;
        }
    }
}
