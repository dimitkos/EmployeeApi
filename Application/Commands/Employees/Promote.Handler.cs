using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Employees
{
    class PromoteHandler : IRequestHandler<Promote, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<long, Employee> _retrieval;
        private readonly ILogger<PromoteHandler> _logger;

        public PromoteHandler(IEmployeePersistence persistence, IEntityRetrieval<long, Employee> retrieval, ILogger<PromoteHandler> logger)
        {
            _persistence = persistence;
            _retrieval = retrieval;
            _logger = logger;
        }

        public async Task<Unit> Handle(Promote request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.Promote();

            await _persistence.Promote(modification);

            _logger.LogInformation("Employee with Id: {id} promoted", modification.Id);

            return Unit.Value;
        }
    }
}
