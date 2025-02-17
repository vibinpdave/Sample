using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using MediatR;
using HRMS.Application.Features.Authentication.Login.Commands;

namespace HRMS.API.Filters
{
    public class BasicAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly IMediator _mediator;

        public BasicAuthenticationFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Check if the Authorization header is present
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            // Ensure the header uses the Basic scheme
            if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                // Decode the Base64-encoded credentials
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

                // Split the credentials into username and password
                var credentials = decodedCredentials.Split(':', 2);
                if (credentials.Length != 2)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var username = credentials[0];
                var password = credentials[1];

                // Send the login command to validate the user (awaited properly)
                var loginCommand = new LoginCommand(username, password);
                var isValidUser = await _mediator.Send(loginCommand); // Use async await

                if (!isValidUser)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Add username as a claim in the HttpContext.User
                var claims = new[] {
                    new Claim(ClaimTypes.Name, username)
                };
                var identity = new ClaimsIdentity(claims, "Basic");
                var principal = new ClaimsPrincipal(identity);
                context.HttpContext.User = principal;
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                context.Result = new UnauthorizedResult();
            }

            await Task.CompletedTask;
        }
    }
}
