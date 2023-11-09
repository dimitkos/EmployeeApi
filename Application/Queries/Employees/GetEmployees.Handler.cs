using Application.Services.Infrastructure;
using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    class GetEmployeesHandler : IRequestHandler<GetEmployees, EmployeeModel[]>
    {
        private readonly IQueryPersistence<GetEmployees, EmployeeModel[]> _persistence;

        public GetEmployeesHandler(IQueryPersistence<GetEmployees, EmployeeModel[]> persistence)
        {
            _persistence = persistence;
        }

        public async Task<EmployeeModel[]> Handle(GetEmployees request, CancellationToken cancellationToken)
            => await _persistence.Fetch(request);
    }
}
