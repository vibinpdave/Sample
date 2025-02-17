using HRMS.Application.Contracts.Persistence;
using MediatR;

namespace HRMS.Application.Features.Authentication.Login.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public LoginCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Fetch user from database
            var user = await _employeeRepository.GetUserByUsernameAndPasswordAsync(request.Username, request.Password);

            if (user == null)
            {
                return false; // User does not exist
            }

            return true;
        }
    }
}
