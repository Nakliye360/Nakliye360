using Mapster;
using Nakliye360.Application.Models.DTOs.CustomerManagement;
using Nakliye360.Domain.Entities.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakliye360.Application.Mapping.CustomerManagement;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerDto>().TwoWays();
        config.NewConfig<CreateCustomerDto, Customer>();
        config.NewConfig<UpdateCustomerDto, Customer>();
        //config.NewConfig<Customer, CustomerDto>()
        //    .Map(dest => dest.IdentityNumber, src => src.IdentityNumber)
        //    .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
        //    .Map(dest => dest.Email, src => src.Email)
        //    .Map(dest => dest.Address, src => src.Address)
        //    .Map(dest => dest.CustomerType, src => src.CustomerType);

        //config.NewConfig<Models.DTOs.CustomerManagement.CustomerDto, Domain.Entities.CustomerManagement.Customer>()
        //    .Map(dest => dest.IdentityNumber, src => src.IdentityNumber)
        //    .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
        //    .Map(dest => dest.Email, src => src.Email)
        //    .Map(dest => dest.Address, src => src.Address)
        //    .Map(dest => dest.CustomerType, src => src.CustomerType);
    }
}
