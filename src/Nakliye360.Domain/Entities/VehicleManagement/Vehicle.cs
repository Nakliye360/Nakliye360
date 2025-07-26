using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.VehicleManagement
{
    /// <summary>
    /// Represents a vehicle used for transportation.
    /// </summary>
    public class Vehicle : BaseEntity
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        /// <summary>
        /// Maximum load capacity of the vehicle (e.g., in kilograms).
        /// </summary>
        public decimal Capacity { get; set; }
        public VehicleStatus Status { get; set; } = VehicleStatus.Available;
    }
}
