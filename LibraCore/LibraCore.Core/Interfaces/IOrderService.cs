using LibraCore.ViewModels.Order;

namespace LibraCore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId);
    }
}
