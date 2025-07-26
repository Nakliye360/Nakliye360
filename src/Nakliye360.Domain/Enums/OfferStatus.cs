using System;

namespace Nakliye360.Domain.Enums
{
    /// <summary>
    /// Represents the current state of an offer made by a carrier on a load request.
    /// </summary>
    public enum OfferStatus
    {
        /// <summary>
        /// Offer has been created and awaits action from the customer.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Customer accepted the offer.  A shipment can now be scheduled.
        /// </summary>
        Accepted = 1,

        /// <summary>
        /// Customer rejected the offer.
        /// </summary>
        Rejected = 2
    }
}