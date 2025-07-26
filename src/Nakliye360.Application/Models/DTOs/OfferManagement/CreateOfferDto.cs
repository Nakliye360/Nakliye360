using System;

namespace Nakliye360.Application.Models.DTOs.OfferManagement
{
    /// <summary>
    /// Model used by carriers to create a new offer for a specific load request.
    /// </summary>
    public class CreateOfferDto
    {
        /// <summary>
        /// The load request this offer applies to.
        /// </summary>
        public Guid LoadRequestId { get; set; }

        /// <summary>
        /// The proposed price for carrying the load.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Optional comments or details about the offer.
        /// </summary>
        public string? Comment { get; set; }
    }
}