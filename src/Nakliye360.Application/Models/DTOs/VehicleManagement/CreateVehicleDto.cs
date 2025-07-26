using System.ComponentModel.DataAnnotations;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.VehicleManagement
{
    public class CreateVehicleDto
    {
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public decimal Capacity { get; set; }
        public VehicleStatus Status { get; set; } = VehicleStatus.Available;
    }
}
