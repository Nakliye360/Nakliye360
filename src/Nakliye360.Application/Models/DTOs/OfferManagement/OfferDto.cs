using System;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.OfferManagement
{
    /// <summary>
    /// Data transfer model for returning offer information via API.
    /// </summary>
    public class OfferDto
    {
        public Guid Id { get; set; }
        public Guid LoadRequestId { get; set; }
        public Guid CarrierId { get; set; }
        public decimal Price { get; set; }
        public string? Comment { get; set; }
        public DateTime OfferedDate { get; set; }
        public OfferStatus Status { get; set; }
    }
}