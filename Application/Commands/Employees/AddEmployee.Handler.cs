using Application.Services.Infrastructure;
using Common;
using Domain.Aggregates;
using Domain.Values;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Employees
{
    class AddEmployeeHandler : IRequestHandler<AddEmployee, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IIdGenerator _idGenerator;
        private readonly ILogger<AddEmployeeHandler> _logger;

        public AddEmployeeHandler(
            IEmployeePersistence persistence,
            IIdGenerator idGenerator,
            ILogger<AddEmployeeHandler> logger)
        {
            _persistence = persistence;
            _logger = logger;
            _idGenerator = idGenerator;
        }

        public async Task<Unit> Handle(AddEmployee request, CancellationToken cancellationToken)
        {
            var employee = Employee.Create(
                id: _idGenerator.GenerateId(),
                name: request.Payload.Name,
                surname: request.Payload.Surname,
                gender: request.Payload.Gender,
                salary: request.Payload.Salary,
                email: request.Payload.Email,
                phoneNumber: new PhoneNumber(phone: request.Payload.PhoneNumber.Phone, mobilePhone: request.Payload.PhoneNumber.MobilePhone));

            await _persistence.AddEmployee(employee);

            _logger.LogInformation("Employee with Id: {id} inserted: {@employee}", employee.Id, employee);

            return Unit.Value;
        }
    }
}
