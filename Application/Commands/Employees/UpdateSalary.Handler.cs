using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Employees
{
    class UpdateSalaryHandler : IRequestHandler<UpdateSalary, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<long, Employee> _retrieval;
        private readonly ILogger<UpdateSalaryHandler> _logger;

        public UpdateSalaryHandler(IEmployeePersistence persistence, IEntityRetrieval<long, Employee> retrieval, ILogger<UpdateSalaryHandler> logger)
        {
            _persistence = persistence;
            _retrieval = retrieval;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateSalary request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.Payload.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.UpdateSalary(request.Payload.Salary);

            await _persistence.UpdateSalary(modification);

            _logger.LogInformation("Employee with Id: {id} change salary : {salary}", modification.Id, modification.Salary);

            return Unit.Value;
        }
    }
}
