using FluentValidation;
using Nakliye360.Application.Models.DTOs.DriverManagement;

namespace Nakliye360.Application.Validators.DriverManagement
{
    /// <summary>
    /// FluentValidation rules for creating a new driver.  Ensures personal information and contact
    /// details are provided and formatted correctly.
    /// </summary>
    public class CreateDriverDtoValidator : AbstractValidator<CreateDriverDto>
    {
        public CreateDriverDtoValidator()
        {
            // First name is required and has a maximum length.
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            // Last name is required and has a maximum length.
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            // License number is required and must have a reasonable length.
            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("Ehliyet numarası boş olamaz.")
                .MaximumLength(50).WithMessage("Ehliyet numarası en fazla 50 karakter olabilir.");

            // Phone number is required and must match a simple phone pattern.
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?[0-9\s\-]{10,15}$").WithMessage("Geçerli bir telefon numarası girin.");

            // VehicleId is optional; if provided it must be a positive integer.
            RuleFor(x => x.VehicleId)
                .GreaterThan(0).When(x => x.VehicleId.HasValue).WithMessage("Geçerli bir araç kimliği seçiniz.");
        }
    }
}