using HRMS.API.Filters;
using HRMS.Application.Features.Authentication.AccessToken.Commands;
using HRMS.Application.Features.Authentication.RefreshToken.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Private Variables
        private readonly IMediator _mediator;
        #endregion

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        [HttpPost]
        [Route("Login")]
        [ServiceFilter(typeof(BasicAuthenticationFilter))]
        public async Task<ActionResult> Login()
        {
            var username = User?.Identity?.Name;
            var command = new TokenCommand(username);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        #endregion

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken()
        {
            var request = HttpContext.Request;
            string accessToken = request.Headers["Authorization"];
            string refreshToken = request.Headers["refresh_token"];
            accessToken = accessToken?.Replace("Bearer ", "");
            refreshToken = refreshToken ?? string.Empty; // Handle case where the refresh token might not exist
            var command = new RefreshTokenCommand(accessToken, refreshToken);

            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
