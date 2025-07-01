namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class LoginRequestModel
    {
        public required string UsernameOrEmail { get; set; }

        public required string Password { get; set; }
    }
}
