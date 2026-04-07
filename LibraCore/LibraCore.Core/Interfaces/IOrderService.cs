using LibraCore.Infrastructure.Data.Enums;
using LibraCore.ViewModels.Order;

namespace LibraCore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId);

        Task<OrderDetailsViewModel?> GetOrderDetailsAsync(Guid id);

        Task CreateOrderAsync(Guid bookId, string userId);

        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();

        Task UpdateOrderStatusAsync(Guid id, OrderStatus newStatus);
    }
}
