using System.Text.Json.Serialization;

namespace Nakliye360.Application.Models.DTOs.Authentication.ResponseModel
{
    public class RegisterUserResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}
