using FluentValidation;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Models.DTOs.Companies;

namespace Nakliye360.Application.Validators.Companies;

public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
{
    public CreateCompanyDtoValidator(ICustomerService customerService)
    {

    }
}

