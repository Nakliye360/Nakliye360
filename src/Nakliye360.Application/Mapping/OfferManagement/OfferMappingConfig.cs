using Mapster;
using Nakliye360.Application.Models.DTOs.OfferManagement;
using Nakliye360.Domain.Entities.OfferManagement;

namespace Nakliye360.Application.Mapping.OfferManagement
{
    /// <summary>
    /// Registers Mapster mappings for the Offer domain entity and its DTO counterparts.
    /// </summary>
    public class OfferMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Offer, OfferDto>();
            config.NewConfig<CreateOfferDto, Offer>();
            config.NewConfig<UpdateOfferDto, Offer>();
        }
    }
}