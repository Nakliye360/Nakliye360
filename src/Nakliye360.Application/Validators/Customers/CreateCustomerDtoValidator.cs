using FluentValidation;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Models.DTOs.CustomerManagement;

namespace Nakliye360.Application.Validators.CustomerManagement;

public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerDtoValidator(ICustomerService customerService)
    {

        RuleFor(x => x.IdentityNumber)
             .NotEmpty().WithMessage("Kimlik numarası boş olamaz.")
             .Length(10, 20).WithMessage("Kimlik numarası 10-20 karakter arasında olmalı.");
        RuleFor(x => x.IdentityNumber)
         .Must(identityNumber => !customerService.ExistsByIdentityNumberAsync(identityNumber).GetAwaiter().GetResult())
         .WithMessage("Bu kimlik numarası zaten kayıtlı.");


        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
            .Matches(@"^\+?[0-9\s\-]{10,15}$").WithMessage("Geçerli bir telefon numarası girin.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Geçerli bir e-posta adresi girin.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres bilgisi boş olamaz.")
            .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir.");

        RuleFor(x => x.CustomerType)
            .IsInEnum().WithMessage("Geçersiz müşteri tipi.");
    }
}