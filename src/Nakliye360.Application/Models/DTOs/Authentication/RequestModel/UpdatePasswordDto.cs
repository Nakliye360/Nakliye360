namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class UpdatePasswordDto
    {
        public required string UserId { get; set; }
        public required string ResetToken { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
