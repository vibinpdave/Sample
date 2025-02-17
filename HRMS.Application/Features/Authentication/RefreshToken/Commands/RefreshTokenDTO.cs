namespace HRMS.Application.Features.Authentication.RefreshToken.Commands
{
    public class RefreshTokenDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
