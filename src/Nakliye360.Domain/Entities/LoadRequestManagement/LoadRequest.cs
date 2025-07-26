using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.LoadRequestManagement
{
    /// <summary>
    /// Represents a request posted by a user indicating that they have a load to be transported.
    /// </summary>
    public class LoadRequest : BaseEntity
    {
        /// <summary>
        /// The customer who created the load request.  This may be null if the requester is not registered as a customer yet.
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// Indicates whether this load is an outbound or return trip.
        /// </summary>
        public LoadDirection Direction { get; set; } = LoadDirection.Outbound;

        /// <summary>
        /// The general type of cargo (e.g., Earthwork, Goods, Food).
        /// </summary>
        public LoadType LoadType { get; set; } = LoadType.Unknown;

        /// <summary>
        /// The type of vehicle required for transporting this load.
        /// </summary>
        public VehicleType VehicleType { get; set; } = VehicleType.Unknown;

        /// <summary>
        /// The approximate weight of the load (e.g., kilograms or tonnes depending on system conventions).
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Additional description provided by the requester for special instructions or details.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The location where the load should be picked up.
        /// </summary>
        public string PickupLocation { get; set; }

        /// <summary>
        /// The destination where the load should be delivered.
        /// </summary>
        public string DeliveryLocation { get; set; }

        /// <summary>
        /// The date and time when this load request was created.  Defaults to UtcNow.
        /// </summary>
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional date when the load is expected to be picked up.  Can be null for immediate availability.
        /// </summary>
        public DateTime? PickupDate { get; set; }

        /// <summary>
        /// Current status of the load request.
        /// </summary>
        public LoadRequestStatus Status { get; set; } = LoadRequestStatus.Open;
    }
}
