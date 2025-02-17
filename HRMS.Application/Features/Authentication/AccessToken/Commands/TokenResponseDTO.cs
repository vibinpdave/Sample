namespace HRMS.Application.Features.Authentication.AccessToken.Commands
{
    public class TokenResponseDTO
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
