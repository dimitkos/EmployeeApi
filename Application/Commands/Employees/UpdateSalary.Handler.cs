﻿using Application.Services.Infrastructure;
using Domain.Aggregates;
using MediatR;

namespace Application.Commands.Employees
{
    public class UpdateSalaryHandler : IRequestHandler<UpdateSalary, Unit>
    {
        private readonly IEmployeePersistence _persistence;
        private readonly IEntityRetrieval<Employee, int> _retrieval;

        public UpdateSalaryHandler(IEmployeePersistence persistence, IEntityRetrieval<Employee, int> retrieval)
        {
            _persistence = persistence;
            _retrieval = retrieval;
        }

        public async Task<Unit> Handle(UpdateSalary request, CancellationToken cancellationToken)
        {
            var employee = await _retrieval.TryRetrieve(request.Payload.EmployeeId);

            if (employee is null)
                return Unit.Value;

            var modification = employee.UpdateSalary(request.Payload.Salary);

            await _persistence.UpdateSalary(modification);

            return Unit.Value;
        }
    }
}
