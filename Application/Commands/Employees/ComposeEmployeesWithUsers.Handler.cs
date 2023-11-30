using Gateway;
using Gateway.Api;
using Gateway.Requests.Users;
using Gateway.Responses.Users;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Commands.Employees
{
    public class ComposeEmployeesWithUsersHandler : IRequestHandler<ComposeEmployeesWithUsers, Unit>
    {
        private readonly IApiClient<GetAllUsers, User[]> _apiClient;
        private readonly ApiConfiguration _apiConfiguration;

        public ComposeEmployeesWithUsersHandler(IApiClient<GetAllUsers, User[]> apiClient, IOptions<ApiConfiguration> options)
        {
            _apiClient = apiClient;
            _apiConfiguration = options.Value;
        }

        public async Task<Unit> Handle(ComposeEmployeesWithUsers request, CancellationToken cancellationToken)
        {
            var url = _apiConfiguration.Host + "users";
            var users = await _apiClient.Get(url);

            return Unit.Value;
        }
    }
}
