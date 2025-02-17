using HRMS.Application.Contracts.Identity;
using HRMS.Application.Features.Authentication.RefreshToken.Commands;
using MediatR;

namespace HRMS.Application.Features.Authentication.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDTO>
    {
        private readonly ITokenService _tokenService;
        public RefreshTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokens = await _tokenService.RefreshTokensAsync(request.accessToken, request.refreshToken);
            return new RefreshTokenDTO
            {
                AccessToken = tokens.NewAccessToken,
                RefreshToken = tokens.NewRefreshToken
            };
        }
    }
}
