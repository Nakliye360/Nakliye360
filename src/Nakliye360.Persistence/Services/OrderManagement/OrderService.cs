using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.OrderManagement;
using Nakliye360.Application.Models.DTOs.OrderManagement;
using Nakliye360.Domain.Entities.OrderManagement;
using Nakliye360.Domain.Enums;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Services.OrderManagement
{
    /// <summary>
    /// Implementation of order operations using Entity Framework Core.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly Nakliye360DbContext _context;
        private readonly IMapper _mapper;

        public OrderService(Nakliye360DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = dto.OrderDate,
                Status = OrderStatus.Pending
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            // map items
            foreach (var itemDto in dto.Items)
            {
                var item = new OrderItem
                {
                    OrderId = order.Id,
                    Description = itemDto.Description,
                    Quantity = itemDto.Quantity,
                    Weight = itemDto.Weight
                };
                await _context.OrderItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return;
            // remove items
            var items = await _context.OrderItems.Where(i => i.OrderId == id).ToListAsync();
            _context.OrderItems.RemoveRange(items);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
            return orders.Adapt<List<OrderDto>>();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order?.Adapt<OrderDto>();
        }

        public async Task<string> UpdateOrderAsync(UpdateOrderDto dto)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == dto.Id);
            if (order == null) return "Order not found";
            order.CustomerId = dto.CustomerId;
            order.OrderDate = dto.OrderDate;
            order.Status = dto.Status;
            // update items: simple approach remove existing and add new
            var existingItems = await _context.OrderItems.Where(i => i.OrderId == dto.Id).ToListAsync();
            _context.OrderItems.RemoveRange(existingItems);
            foreach (var itemDto in dto.Items)
            {
                var item = new OrderItem
                {
                    OrderId = dto.Id,
                    Description = itemDto.Description,
                    Quantity = itemDto.Quantity,
                    Weight = itemDto.Weight
                };
                await _context.OrderItems.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return "Order updated successfully.";
        }
    }
}
