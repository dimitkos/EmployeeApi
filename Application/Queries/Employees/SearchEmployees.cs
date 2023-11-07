using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    public class SearchEmployees : IRequest<EmployeeModel[]>
    {
        public EmployeeSearchPayload Payload { get; }

        public SearchEmployees(EmployeeSearchPayload payload)
        {
            Payload = payload;
        }
    }
}
