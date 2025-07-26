using Mapster;
using Nakliye360.Application.Models.DTOs.DriverManagement;
using Nakliye360.Domain.Entities.DriverManagement;

namespace Nakliye360.Application.Mapping.DriverManagement
{
    public class DriverMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Driver, DriverDto>();
        }
    }
}
