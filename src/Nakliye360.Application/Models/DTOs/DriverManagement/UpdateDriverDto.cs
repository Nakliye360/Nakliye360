using System.ComponentModel.DataAnnotations;

namespace Nakliye360.Application.Models.DTOs.DriverManagement
{
    public class UpdateDriverDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int? VehicleId { get; set; }
    }
}
