using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Data.Enums;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Order;

namespace LibraCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IBookRepository bookRepository;

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            this.orderRepository = orderRepository;
            this.bookRepository = bookRepository;
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

        public async Task CreateOrderAsync(Guid bookId, string userId)
        {
            Book? book = await bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                throw new EntityNotFoundException();
            }

            OrderItem orderItem = new OrderItem
            {
                BookId = bookId,
                Quantity = 1,
                PriceAtPurchase = book.Price
            };

            Order order = new Order();

            order.UserId = Guid.Parse(userId);
            order.TotalPrice = book.Price;
            order.OrderItems.Add(orderItem);

            bool success = await orderRepository.AddOrderAsync(order);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            IEnumerable<Order> orders = await orderRepository.GetAllOrdersAsync();

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

        public async Task UpdateOrderStatusAsync(Guid id, OrderStatus newStatus)
        {
            Order? order = await orderRepository
                .GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new EntityNotFoundException();
            }

            order.Status = newStatus;

            bool success = await orderRepository.UpdateOrderStatusAsync(order);
            if (!success)
            {
                throw new EntityPersistFailureException();
            }
        }
    }
}
