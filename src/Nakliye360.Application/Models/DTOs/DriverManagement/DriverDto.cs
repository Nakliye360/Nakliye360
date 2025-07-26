namespace Nakliye360.Application.Models.DTOs.DriverManagement
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int? VehicleId { get; set; }
    }
}
