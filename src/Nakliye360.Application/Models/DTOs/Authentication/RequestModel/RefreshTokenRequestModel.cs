namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class RefreshTokenRequestModel
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int AccessTokenLifeTime { get; set; } = 120; // Default value, can be overridden
    }
}
