using Mapster;
using Nakliye360.Application.Models.DTOs.ShipmentManagement;
using Nakliye360.Domain.Entities.ShipmentManagement;

namespace Nakliye360.Application.Mapping.ShipmentManagement
{
    /// <summary>
    /// Mapster configuration for shipment domain and DTO conversions.
    /// </summary>
    public class ShipmentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Shipment, ShipmentDto>();
            config.ForType<CreateShipmentDto, Shipment>();
            config.ForType<UpdateShipmentDto, Shipment>();
        }
    }
}