using Mapster;
using Nakliye360.Application.Models.DTOs.VehicleManagement;
using Nakliye360.Domain.Entities.VehicleManagement;

namespace Nakliye360.Application.Mapping.VehicleManagement
{
    public class VehicleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Vehicle, VehicleDto>();
        }
    }
}
