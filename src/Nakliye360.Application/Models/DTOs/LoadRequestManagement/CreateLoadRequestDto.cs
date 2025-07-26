using System;
using System.ComponentModel.DataAnnotations;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.LoadRequestManagement
{
    /// <summary>
    /// DTO used when creating a new load request.  Contains only the fields that are required from the client.
    /// </summary>
    public class CreateLoadRequestDto
    {
        /// <summary>
        /// The customer who posts the load request (nullable if not registered yet).
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// The direction of the load.  Required field.
        /// </summary>
        [Required]
        public LoadDirection Direction { get; set; }

        /// <summary>
        /// Type of the cargo (e.g., Earthwork, Goods, Food).  Required field.
        /// </summary>
        [Required]
        public LoadType LoadType { get; set; }

        /// <summary>
        /// Type of vehicle needed to carry this load.  Required field.
        /// </summary>
        [Required]
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// Approximate weight of the load.  Required and must be positive.
        /// </summary>
        [Required]
        public decimal Weight { get; set; }

        /// <summary>
        /// Additional notes or instructions.  Optional.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Location for pickup.  Required.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string PickupLocation { get; set; }

        /// <summary>
        /// Location for delivery.  Required.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DeliveryLocation { get; set; }

        /// <summary>
        /// Optional date when the load needs to be picked up.
        /// </summary>
        public DateTime? PickupDate { get; set; }
    }
}
