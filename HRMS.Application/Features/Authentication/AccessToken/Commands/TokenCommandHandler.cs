using HRMS.Application.Contracts.Identity;
using MediatR;

namespace HRMS.Application.Features.Authentication.AccessToken.Commands
{
    public class TokenCommandHandler : IRequestHandler<TokenCommand, TokenResponseDTO>
    {
        private readonly ITokenService _tokenService;
        public TokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDTO> Handle(TokenCommand request, CancellationToken cancellationToken)
        {
            var tokens = await _tokenService.GenerateTokensAsync(request.UserName);
            return new TokenResponseDTO
            {
                UserName = request.UserName,
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }
    }
}
