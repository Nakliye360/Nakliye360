using Mapster;
using Nakliye360.Application.Models.DTOs.Companies;
using Nakliye360.Application.Models.DTOs.CustomerManagement;
using Nakliye360.Domain.Entities.Companies;

namespace Nakliye360.Application.Mapping.Companies;

public class CompanyMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Company, CompanyDto>().TwoWays();
        config.NewConfig<Company, CreateCompanyDto>().TwoWays();


    }
}
