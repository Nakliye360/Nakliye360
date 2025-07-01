using System.Text.Json.Serialization;

namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class RegisterDto
    {
      
        [JsonPropertyName("userName")]
        public required string Username { get; set; }
        [JsonPropertyName("email")]
        public required string Email { get; set; }
        [JsonPropertyName("password")]
        public required string Password { get; set; }
        [JsonPropertyName("confirmPassword")]
        public required string PasswordConfirm { get; set; }
    }
}
