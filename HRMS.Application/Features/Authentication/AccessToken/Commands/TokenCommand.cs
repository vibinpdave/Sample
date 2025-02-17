using MediatR;

namespace HRMS.Application.Features.Authentication.AccessToken.Commands
{
    public record TokenCommand(string UserName) : IRequest<TokenResponseDTO>;
}
