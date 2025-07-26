using System;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.OfferManagement
{
    /// <summary>
    /// Represents an offer from a carrier to carry a given load request.  A load request can have multiple offers from different carriers.
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// Primary key for the offer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key to the associated load request.
        /// </summary>
        public Guid LoadRequestId { get; set; }

        /// <summary>
        /// Identifier of the carrier (AppUser.Id) making the offer.
        /// </summary>
        public Guid CarrierId { get; set; }

        /// <summary>
        /// Proposed price for carrying the load.  The unit and currency should be agreed upon by the parties.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Optional note or comments included by the carrier with the offer.
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// The timestamp when the offer was created.
        /// </summary>
        public DateTime OfferedDate { get; set; }

        /// <summary>
        /// Current status of the offer (pending, accepted, rejected).
        /// </summary>
        public OfferStatus Status { get; set; }
    }
}