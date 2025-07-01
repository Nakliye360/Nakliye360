using FluentValidation;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Models.DTOs.CustomerManagement;

namespace Nakliye360.Application.Validators.CustomerManagement;

public class UpdateCustomerDtoValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerDtoValidator(ICustomerService customerService)
    {
        RuleFor(x => x.IdentityNumber)
            .NotEmpty().WithMessage("Kimlik numarası boş olamaz.")
            .MaximumLength(20);

        RuleFor(x => x.IdentityNumber)
       .Must(identityNumber => !customerService.ExistsByIdentityNumberAsync(identityNumber).GetAwaiter().GetResult())
       .WithMessage("Bu kimlik numarası zaten kayıtlı.");


        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
            .MaximumLength(15);

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz.")
            .MaximumLength(500);

        RuleFor(x => x.CustomerType)
            .IsInEnum().WithMessage("Geçerli bir müşteri tipi seçilmelidir.");
    }
}
