
using HRMS.Application.Contracts.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS.Infrastructure.Token
{
    public class JWTTokenService: ITokenService
    {
        #region Private Variables
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public JWTTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Token Generation
        public async Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(string username)
        {
            var accessToken = await GenerateAccessTokenAsync(username);
            var refreshToken = await GenerateRefreshTokenAsync(accessToken);

            return (AccessToken: accessToken, RefreshToken: refreshToken);
        }
        #endregion

        #region GenerateAccessToken
        private async Task<string> GenerateAccessTokenAsync(string username)
        {
            return await Task.Run(() =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim("username", username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
            };

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"])),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }
        #endregion

        #region GenerateRefreshToken
        public async Task<string> GenerateRefreshTokenAsync(string accessToken)
        {
            return await Task.Run(() =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim("accessToken", accessToken),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                };

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:RefreshTokenExpirationMinutes"])),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }

        #endregion

        #region RefreshTokens
        public async Task<(string NewAccessToken, string NewRefreshToken)> RefreshTokensAsync(string accessToken, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                // Run validation and token handling asynchronously
                return await Task.Run(() =>
                {
                    var refreshTokenPrincipal = tokenHandler.ValidateToken(refreshToken, validationParameters, out _);

                    var embeddedAccessToken = refreshTokenPrincipal.Claims.FirstOrDefault(c => c.Type == "accessToken")?.Value;

                    if (embeddedAccessToken != accessToken)
                    {
                        throw new SecurityTokenException("Invalid access token embedded in the refresh token.");
                    }

                    var accessTokenPrincipal = tokenHandler.ValidateToken(accessToken, validationParameters, out var validatedToken);

                    if (validatedToken is JwtSecurityToken jwtToken && jwtToken.ValidTo > DateTime.UtcNow)
                    {
                        throw new SecurityTokenException("Access token is still valid.");
                    }
                    var username = accessTokenPrincipal.FindFirst("username")?.Value;

                    return GenerateTokensAsync(username).Result;
                });
            }
            catch (SecurityTokenException ex)
            {
                throw;  // Rethrow the same exception to preserve its details
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid refresh or access token.", ex);
            }
        }

        #endregion
    }
}
