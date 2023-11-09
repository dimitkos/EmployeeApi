using Application.Services.Infrastructure;
using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    class GetAllEmployeesHandler : IRequestHandler<GetAllEmployees, EmployeeModel[]>
    {
        private readonly IQueryPersistence<GetAllEmployees, EmployeeModel[]> _persistence;

        public GetAllEmployeesHandler(IQueryPersistence<GetAllEmployees, EmployeeModel[]> persistence)
        {
            _persistence = persistence;
        }

        public async Task<EmployeeModel[]> Handle(GetAllEmployees request, CancellationToken cancellationToken) 
            => await _persistence.Fetch(request);
    }
}
