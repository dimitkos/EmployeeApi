using Application.Services.Infrastructure;
using MediatR;

namespace Application.Commands.Employees
{
    class DeleteEmployeesHandler : IRequestHandler<DeleteEmployees, Unit>
    {
        private readonly IEmployeePersistence _persistence;

        public DeleteEmployeesHandler(IEmployeePersistence persistence)
        {
            _persistence = persistence;
        }

        public async Task<Unit> Handle(DeleteEmployees request, CancellationToken cancellationToken)
        {
            await _persistence.DeleteEmployees(request.EmployeeIds);

            return Unit.Value;
        }
    }
}
