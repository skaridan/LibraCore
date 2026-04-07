using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Order;

namespace LibraCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(string userId)
        {
            Guid userGuid = Guid.Parse(userId);

            IEnumerable<Order> orders = await orderRepository
               .GetUserOrdersAsync(userGuid);

            IEnumerable<OrderViewModel> orderViewModels = orders
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice,
                    Status = o.Status.ToString()
                })
                .OrderByDescending(o => o.OrderDate)
                .ToArray();

            return orderViewModels;
        }
    }
}
