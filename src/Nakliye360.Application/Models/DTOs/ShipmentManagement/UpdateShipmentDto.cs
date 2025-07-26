using System;
using System.ComponentModel.DataAnnotations;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.ShipmentManagement
{
    /// <summary>
    /// DTO for updating an existing shipment.
    /// </summary>
    public class UpdateShipmentDto
    {
        [Required]
        public int Id { get; set; }
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
        public ShipmentStatus Status { get; set; }
    }
}