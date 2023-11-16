using Application.Services.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Employees
{
    class DeleteEmployeesHandler : IRequestHandler<DeleteEmployees, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly ILogger<DeleteEmployeesHandler> _logger;

        public DeleteEmployeesHandler(IEmployeePersistence persistence, ILogger<DeleteEmployeesHandler> logger)
        {
            _persistence = persistence;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEmployees request, CancellationToken cancellationToken)
        {
            await _persistence.DeleteEmployees(request.EmployeeIds);

            _logger.LogInformation("Employees with Ids: {ids} deleted", request.EmployeeIds);

            return Unit.Value;
        }
    }
}
