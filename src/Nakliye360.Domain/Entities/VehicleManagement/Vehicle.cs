using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.VehicleManagement
{
    /// <summary>
    /// Represents a vehicle used for transportation.  Extended to include vehicle type for more precise matching with loads.
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

        /// <summary>
        /// Category of the vehicle (e.g., Truck, Van, DumpTruck).  Helps match vehicles to appropriate loads.
        /// </summary>
        public VehicleType VehicleType { get; set; } = VehicleType.Unknown;
    }
}
