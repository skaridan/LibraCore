using LibraCore.ViewModels.Order;

namespace LibraCore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId);

        Task<OrderDetailsViewModel?> GetOrderDetailsAsync(Guid id);

        Task CreateOrderAsync(Guid bookId, string userId);
    }
}
