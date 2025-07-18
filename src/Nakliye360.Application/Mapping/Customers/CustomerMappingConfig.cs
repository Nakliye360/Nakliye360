using Mapster;
using Nakliye360.Application.Models.DTOs.CustomerManagement;
using Nakliye360.Domain.Entities.CustomerManagement;

namespace Nakliye360.Application.Mapping.CustomerManagement;

public class CustomerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Ana Customer Mapping (TwoWay)
        config.NewConfig<Customer, CustomerDto>().TwoWays();

        // Create & Update DTO -> Customer
        config.NewConfig<CreateCustomerDto, Customer>();
        config.NewConfig<UpdateCustomerDto, Customer>();

        // Alt Entity Mapping (opsiyonel ama önerilir)
        config.NewConfig<IndividualCustomerDto, IndividualCustomer>().TwoWays();
        config.NewConfig<CorporateCustomerDto, CorporateCustomer>().TwoWays();
        config.NewConfig<ProducerCustomerDto, ProducerCustomer>().TwoWays();
    }
}
