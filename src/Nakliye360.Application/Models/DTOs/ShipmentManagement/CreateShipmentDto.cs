using System;
using System.ComponentModel.DataAnnotations;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.ShipmentManagement
{
    /// <summary>
    /// Data transfer object for creating a new shipment.
    /// </summary>
    public class CreateShipmentDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public int DriverId { get; set; }
        [Required]
        public string PickupLocation { get; set; }
        [Required]
        public string DeliveryLocation { get; set; }
        [Required]
        public DateTime PickupDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
    }
}