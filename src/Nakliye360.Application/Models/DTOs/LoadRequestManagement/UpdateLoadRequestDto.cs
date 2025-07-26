using System;
using System.ComponentModel.DataAnnotations;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.LoadRequestManagement
{
    /// <summary>
    /// DTO used when updating an existing load request.
    /// </summary>
    public class UpdateLoadRequestDto
    {
        /// <summary>
        /// Unique identifier of the load request being updated.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        [Required]
        public LoadDirection Direction { get; set; }

        [Required]
        public LoadType LoadType { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        public decimal Weight { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(200)]
        public string PickupLocation { get; set; }

        [Required]
        [StringLength(200)]
        public string DeliveryLocation { get; set; }

        public DateTime? PickupDate { get; set; }

        /// <summary>
        /// Updated status of the load request (optional; will be preserved if not provided).
        /// </summary>
        public LoadRequestStatus? Status { get; set; }
    }
}
