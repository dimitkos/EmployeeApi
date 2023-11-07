using MediatR;
using Shared;

namespace Application.Queries.Employees
{
    public class GetAllEmployees : IRequest<EmployeeModel[]>
    {
    }
}
