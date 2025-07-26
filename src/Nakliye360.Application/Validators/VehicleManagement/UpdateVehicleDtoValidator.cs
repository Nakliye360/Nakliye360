using FluentValidation;
using Nakliye360.Application.Models.DTOs.VehicleManagement;

namespace Nakliye360.Application.Validators.VehicleManagement
{
    /// <summary>
    /// Validator for updating vehicles.  Validates identifier and other fields.
    /// </summary>
    public class UpdateVehicleDtoValidator : AbstractValidator<UpdateVehicleDto>
    {
        public UpdateVehicleDtoValidator()
        {
            // Id must be greater than zero.
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz araç kimliği.");

            // License plate remains required on update.
            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("Plaka boş olamaz.")
                .Length(2, 20).WithMessage("Plaka 2 ile 20 karakter arasında olmalıdır.");

            // Brand can be optional on update but if provided should be limited in length.
            RuleFor(x => x.Brand)
                .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.Brand))
                .WithMessage("Marka en fazla 50 karakter olabilir.");

            // Model can be optional on update but if provided should be limited in length.
            RuleFor(x => x.Model)
                .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.Model))
                .WithMessage("Model en fazla 50 karakter olabilir.");

            // Capacity must still be positive if present.  0 is accepted to leave unchanged.
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır.");

            // Status must be valid enum.
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Geçersiz araç durumu seçildi.");
        }
    }
}