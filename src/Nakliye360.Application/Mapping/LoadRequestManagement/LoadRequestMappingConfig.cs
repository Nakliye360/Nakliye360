using Mapster;
using Nakliye360.Application.Models.DTOs.LoadRequestManagement;
using Nakliye360.Domain.Entities.LoadRequestManagement;

namespace Nakliye360.Application.Mapping.LoadRequestManagement
{
    /// <summary>
    /// Mapster configuration for LoadRequest module.  Specifies how domain entities convert to DTOs and vice versa.
    /// </summary>
    public class LoadRequestMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Domain to DTO mapping
            config.NewConfig<LoadRequest, LoadRequestDto>();

            // DTO to domain mappings are handled implicitly by Mapster's Adapt method.  No additional configuration needed here.
        }
    }
}
