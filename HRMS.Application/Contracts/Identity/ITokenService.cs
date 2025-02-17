
namespace HRMS.Application.Contracts.Identity
{
    public interface ITokenService
    {
        #region GenerateTokensAsync
        /// <summary>
        /// Generates both access and refresh tokens for a user.
        /// </summary>
        /// <param name="username">The username for whom tokens are generated.</param>
        /// <returns>A tuple containing the access token and refresh token.</returns>
        Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(string username);
        #endregion

        #region RefreshTokensAsync
        /// <summary>
        /// Validates and refreshes tokens, generating new access and refresh tokens.
        /// </summary>
        /// <param name="accessToken">The expired or soon-to-expire access token.</param>
        /// <param name="refreshToken">The refresh token to validate.</param>
        /// <returns>A tuple containing the new access token and refresh token.</returns>
        Task<(string NewAccessToken, string NewRefreshToken)> RefreshTokensAsync(string accessToken, string refreshToken); 
        #endregion
    }
}
