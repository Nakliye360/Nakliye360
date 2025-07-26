using Mapster;
using Nakliye360.Application.Models.DTOs.OrderManagement;
using Nakliye360.Domain.Entities.OrderManagement;

namespace Nakliye360.Application.Mapping.OrderManagement
{
    /// <summary>
    /// Mapster configuration for order mappings.
    /// This scans for mapping rules when ApplicationServiceRegistration is invoked.
    /// </summary>
    public class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.Items, src => src.Items);
            config.NewConfig<OrderItem, OrderItemDto>();
        }
    }
}
