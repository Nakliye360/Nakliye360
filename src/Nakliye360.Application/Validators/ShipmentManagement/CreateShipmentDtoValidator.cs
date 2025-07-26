using FluentValidation;
using Nakliye360.Application.Models.DTOs.ShipmentManagement;

namespace Nakliye360.Application.Validators.ShipmentManagement
{
    /// <summary>
    /// Validator for creating shipment records.
    /// </summary>
    public class CreateShipmentDtoValidator : AbstractValidator<CreateShipmentDto>
    {
        public CreateShipmentDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("OrderId must be greater than zero.");
            RuleFor(x => x.VehicleId)
                .GreaterThan(0).WithMessage("VehicleId must be greater than zero.");
            RuleFor(x => x.DriverId)
                .GreaterThan(0).WithMessage("DriverId must be greater than zero.");
            RuleFor(x => x.PickupLocation)
                .NotEmpty().WithMessage("Pickup location is required.")
                .MaximumLength(200).WithMessage("Pickup location cannot exceed 200 characters.");
            RuleFor(x => x.DeliveryLocation)
                .NotEmpty().WithMessage("Delivery location is required.")
                .MaximumLength(200).WithMessage("Delivery location cannot exceed 200 characters.");
            RuleFor(x => x.PickupDate)
                .LessThanOrEqualTo(x => x.DeliveryDate).WithMessage("Pickup date cannot be after the delivery date.");
        }
    }
}