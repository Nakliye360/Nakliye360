using System.Text.Json.Serialization;

namespace Nakliye360.Application.Models.DTOs.Authentication
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
