using System.ComponentModel.DataAnnotations;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.VehicleManagement
{
    public class UpdateVehicleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Capacity { get; set; }
        public VehicleStatus Status { get; set; }

        /// <summary>
        /// Updated category of the vehicle.
        /// </summary>
        public VehicleType VehicleType { get; set; }
    }
}
