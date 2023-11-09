using Application.Services.Infrastructure;
using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    class SearchEmployeesHandler : IRequestHandler<SearchEmployees, EmployeeModel[]>
    {
        private readonly IQueryPersistence<SearchEmployees, EmployeeModel[]> _persistence;

        public SearchEmployeesHandler(IQueryPersistence<SearchEmployees, EmployeeModel[]> persistence)
        {
            _persistence = persistence;
        }

        public async Task<EmployeeModel[]> Handle(SearchEmployees request, CancellationToken cancellationToken)
            => await _persistence.Fetch(request);
    }
}
