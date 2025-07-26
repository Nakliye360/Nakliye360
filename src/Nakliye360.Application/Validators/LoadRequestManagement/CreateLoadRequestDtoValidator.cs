using System;
using FluentValidation;
using Nakliye360.Application.Models.DTOs.LoadRequestManagement;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Validators.LoadRequestManagement
{
    /// <summary>
    /// Validator for creating load requests.  Ensures required fields are present and contain sensible values.
    /// </summary>
    public class CreateLoadRequestDtoValidator : AbstractValidator<CreateLoadRequestDto>
    {
        public CreateLoadRequestDtoValidator()
        {
            // Direction must be provided and within the defined enum values.
            RuleFor(x => x.Direction)
                .IsInEnum().WithMessage("Geçersiz yük yönü seçildi.");

            // LoadType must be provided and valid.
            RuleFor(x => x.LoadType)
                .IsInEnum().WithMessage("Geçersiz yük tipi seçildi.");

            // VehicleType must be provided and valid.
            RuleFor(x => x.VehicleType)
                .IsInEnum().WithMessage("Geçersiz araç tipi seçildi.");

            // Weight must be greater than zero.
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Yük ağırlığı 0'dan büyük olmalıdır.");

            // PickupLocation is required and has a maximum length.
            RuleFor(x => x.PickupLocation)
                .NotEmpty().WithMessage("Alış yeri boş olamaz.")
                .MaximumLength(200).WithMessage("Alış yeri en fazla 200 karakter olabilir.");

            // DeliveryLocation is required and has a maximum length.
            RuleFor(x => x.DeliveryLocation)
                .NotEmpty().WithMessage("Teslim yeri boş olamaz.")
                .MaximumLength(200).WithMessage("Teslim yeri en fazla 200 karakter olabilir.");

            // PickupDate, if provided, must not be in the past.
            RuleFor(x => x.PickupDate)
                .Must(date => !date.HasValue || date.Value.Date >= DateTime.UtcNow.Date)
                .WithMessage("Alış tarihi geçmiş bir tarih olamaz.");
        }
    }
}
