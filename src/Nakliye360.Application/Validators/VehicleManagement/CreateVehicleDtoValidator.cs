using FluentValidation;
using Nakliye360.Application.Models.DTOs.VehicleManagement;

namespace Nakliye360.Application.Validators.VehicleManagement
{
    /// <summary>
    /// FluentValidation validator for creating vehicles.
    /// Ensures required fields are set and sensible ranges are respected.
    /// </summary>
    public class CreateVehicleDtoValidator : AbstractValidator<CreateVehicleDto>
    {
        public CreateVehicleDtoValidator()
        {
            // License plate must be provided and within a reasonable length.
            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("Plaka boş olamaz.")
                .Length(2, 20).WithMessage("Plaka 2 ile 20 karakter arasında olmalıdır.");

            // Brand is required and has a max length.
            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Marka adı boş olamaz.")
                .MaximumLength(50).WithMessage("Marka en fazla 50 karakter olabilir.");

            // Model is required and has a max length.
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model adı boş olamaz.")
                .MaximumLength(50).WithMessage("Model en fazla 50 karakter olabilir.");

            // Capacity must be a positive number.
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır.");

            // Status must be a valid enum value.
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Geçersiz araç durumu seçildi.");

            // VehicleType must be provided and valid.
            RuleFor(x => x.VehicleType)
                .IsInEnum().WithMessage("Geçersiz araç tipi seçildi.");
        }
    }
}
