using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.VehicleManagement
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Capacity { get; set; }
        public VehicleStatus Status { get; set; }
    }
}
