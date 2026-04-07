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

        public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(Guid id)
        {
            Order? order = await orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return null;
            }

            IEnumerable<OrderItemViewModel> items = order.OrderItems
                .Select(oi => new OrderItemViewModel
                {
                    BookTitle = oi.Book.Title,
                    BookImageUrl = oi.Book.ImageUrl,
                    Quantity = oi.Quantity,
                    PriceAtPurchase = oi.PriceAtPurchase
                })
                .ToArray();

            OrderDetailsViewModel viewModel = new OrderDetailsViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status.ToString(),
                Items = items
            };

            return viewModel;
        }
    }
}
