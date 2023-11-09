using Application.Services.Infrastructure;
using Domain.Aggregates;
using Domain.Values;
using MediatR;

namespace Application.Commands.Employees
{
    class AddEmployeeHandler : IRequestHandler<AddEmployee, Unit>
    {
        private readonly IEmployeePersistence _persistence;

        public AddEmployeeHandler(IEmployeePersistence persistence)
        {
            _persistence = persistence;
        }

        public async Task<Unit> Handle(AddEmployee request, CancellationToken cancellationToken)
        {
#warning add id generator
            var employee = Employee.Create(
                id: 1,
                name: request.Payload.Name,
                surname: request.Payload.Surname,
                gender: request.Payload.Gender,
                salary: request.Payload.Salary,
                email:request.Payload.Email,
                phoneNumber: new PhoneNumber(phone: request.Payload.PhoneNumber.Phone, mobilePhone: request.Payload.PhoneNumber.MobilePhone));

#warning add logging
            await _persistence.AddEmployee(employee);

            return Unit.Value;
        }
    }
}
