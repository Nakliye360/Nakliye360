using System;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.ShipmentManagement
{
    /// <summary>
    /// Data transfer object for viewing shipment information.
    /// </summary>
    public class ShipmentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ShipmentStatus Status { get; set; }
    }
}