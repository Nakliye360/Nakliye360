using System;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.LoadRequestManagement
{
    /// <summary>
    /// Data Transfer Object for returning detailed information about a load request.
    /// </summary>
    public class LoadRequestDto
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public LoadDirection Direction { get; set; }
        public LoadType LoadType { get; set; }
        public VehicleType VehicleType { get; set; }
        public decimal Weight { get; set; }
        public string Description { get; set; }
        public string PickupLocation { get; set; }
        public string DeliveryLocation { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public LoadRequestStatus Status { get; set; }
    }
}
