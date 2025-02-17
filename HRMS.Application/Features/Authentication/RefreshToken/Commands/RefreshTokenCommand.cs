using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Features.Authentication.RefreshToken.Commands
{
    public record RefreshTokenCommand(string accessToken, string refreshToken) : IRequest<RefreshTokenDTO>;
}
