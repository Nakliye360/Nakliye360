using FluentValidation;
using Nakliye360.Application.Models.DTOs.DriverManagement;

namespace Nakliye360.Application.Validators.DriverManagement
{
    /// <summary>
    /// Validator for updating driver information.  Validates identifier and basic fields.
    /// </summary>
    public class UpdateDriverDtoValidator : AbstractValidator<UpdateDriverDto>
    {
        public UpdateDriverDtoValidator()
        {
            // Id must be provided and be positive.
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz sürücü kimliği.");

            // First name remains required.
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            // Last name remains required.
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            // License number remains required.
            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("Ehliyet numarası boş olamaz.")
                .MaximumLength(50).WithMessage("Ehliyet numarası en fazla 50 karakter olabilir.");

            // Phone number remains required and must match a simple pattern.
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?[0-9\s\-]{10,15}$").WithMessage("Geçerli bir telefon numarası girin.");

            // VehicleId, if set, must be positive.
            RuleFor(x => x.VehicleId)
                .GreaterThan(0).When(x => x.VehicleId.HasValue).WithMessage("Geçerli bir araç kimliği seçiniz.");
        }
    }
}