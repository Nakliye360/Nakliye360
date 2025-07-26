using System;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.ShipmentManagement
{
    /// <summary>
    /// Represents a shipment which ties an order to a vehicle and a driver, along with pickup and delivery information.
    /// </summary>
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DeliveryLocation { get; set; } = null!;
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;

        // Navigation properties (optional) for EF relationships
        // public Order Order { get; set; }
        // public Vehicle Vehicle { get; set; }
        // public Driver Driver { get; set; }
    }
}