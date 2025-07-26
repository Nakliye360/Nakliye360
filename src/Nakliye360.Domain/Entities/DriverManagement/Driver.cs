namespace Nakliye360.Domain.Entities.DriverManagement
{
    /// <summary>
    /// Represents a driver assigned to orders.
    /// </summary>
    public class Driver : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Optional assigned vehicle id; can be null if unassigned.
        /// </summary>
        public int? VehicleId { get; set; }
    }
}
