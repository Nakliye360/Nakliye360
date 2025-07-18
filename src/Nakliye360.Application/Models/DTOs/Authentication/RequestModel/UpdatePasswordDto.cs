namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class UpdatePasswordDto
    {
        //public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
