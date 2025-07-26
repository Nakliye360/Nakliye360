using System;
using FluentValidation;
using Nakliye360.Application.Models.DTOs.LoadRequestManagement;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Validators.LoadRequestManagement
{
    /// <summary>
    /// Validator for updating load requests.  Ensures id is provided and fields remain valid if supplied.
    /// </summary>
    public class UpdateLoadRequestDtoValidator : AbstractValidator<UpdateLoadRequestDto>
    {
        public UpdateLoadRequestDtoValidator()
        {
            // Id must not be empty Guid.
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("Geçersiz yük talebi kimliği.");

            // Direction must be a valid enum value.
            RuleFor(x => x.Direction)
                .IsInEnum().WithMessage("Geçersiz yük yönü seçildi.");

            // LoadType must be valid.
            RuleFor(x => x.LoadType)
                .IsInEnum().WithMessage("Geçersiz yük tipi seçildi.");

            // VehicleType must be valid.
            RuleFor(x => x.VehicleType)
                .IsInEnum().WithMessage("Geçersiz araç tipi seçildi.");

            // Weight must be positive.
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Yük ağırlığı 0'dan büyük olmalıdır.");

            // PickupLocation required and max length.
            RuleFor(x => x.PickupLocation)
                .NotEmpty().WithMessage("Alış yeri boş olamaz.")
                .MaximumLength(200).WithMessage("Alış yeri en fazla 200 karakter olabilir.");

            // DeliveryLocation required and max length.
            RuleFor(x => x.DeliveryLocation)
                .NotEmpty().WithMessage("Teslim yeri boş olamaz.")
                .MaximumLength(200).WithMessage("Teslim yeri en fazla 200 karakter olabilir.");

            // PickupDate must not be in the past.
            RuleFor(x => x.PickupDate)
                .Must(date => !date.HasValue || date.Value.Date >= DateTime.UtcNow.Date)
                .WithMessage("Alış tarihi geçmiş bir tarih olamaz.");

            // Status, if provided, must be a valid enum value.
            RuleFor(x => x.Status)
                .Must(status => !status.HasValue || Enum.IsDefined(typeof(LoadRequestStatus), status.Value))
                .WithMessage("Geçersiz yük talebi durumu seçildi.");
        }
    }
}
