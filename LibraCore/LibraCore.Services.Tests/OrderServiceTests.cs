using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Data.Enums;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using LibraCore.ViewModels.Order;
using Moq;

namespace LibraCore.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> orderRepoMock = null!;
        private Mock<IBookRepository> bookRepoMock = null!;
        private OrderService orderService = null!;

        [SetUp]
        public void SetUp()
        {
            orderRepoMock = new Mock<IOrderRepository>();
            bookRepoMock = new Mock<IBookRepository>();
            orderService = new OrderService(orderRepoMock.Object, bookRepoMock.Object);
        }

        [Test]
        public async Task CreateOrderAsync_WhenBookExists_ShouldCreateOrderWithCorrectData()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();
            Book book = new Book { Id = bookId, Title = "C# Mastery", Price = 50.00m };

            bookRepoMock.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            orderRepoMock.Setup(r => r.AddOrderAsync(It.IsAny<Order>())).ReturnsAsync(true);

            // Act
            await orderService.CreateOrderAsync(bookId, userId);

            // Assert
            orderRepoMock.Verify(r => r.AddOrderAsync(It.Is<Order>(o =>
                o.UserId == Guid.Parse(userId) &&
                o.TotalPrice == 50.00m &&
                o.OrderItems.Count == 1 &&
                o.OrderItems.First().PriceAtPurchase == 50.00m)), Times.Once);
        }

        [Test]
        public void CreateOrderAsync_WhenBookNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Book?)null);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await orderService.CreateOrderAsync(Guid.NewGuid(), Guid.NewGuid().ToString()));
        }

        [Test]
        public async Task GetOrderDetailsAsync_WhenExists_ShouldMapAllFieldsCorrectly()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Order order = new Order
            {
                Id = orderId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Shipped,
                TotalPrice = 100m,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Book = new Book { Title = "Book 1" }, Quantity = 1, PriceAtPurchase = 100m }
                }
            };

            orderRepoMock.Setup(r => r.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            OrderDetailsViewModel? result = await orderService.GetOrderDetailsAsync(orderId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result!.Id, Is.EqualTo(orderId));
                Assert.That(result.Status, Is.EqualTo("Shipped"));
                Assert.That(result.Items.First().BookTitle, Is.EqualTo("Book 1"));
            });
        }

        [Test]
        public async Task UpdateOrderStatusAsync_WhenOrderExists_ShouldUpdateStatus()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Order order = new Order { Id = orderId, Status = OrderStatus.Pending };

            orderRepoMock.Setup(r => r.GetOrderByIdAsync(orderId)).ReturnsAsync(order);
            orderRepoMock.Setup(r => r.UpdateOrderStatusAsync(order)).ReturnsAsync(true);

            // Act
            await orderService.UpdateOrderStatusAsync(orderId, OrderStatus.Delivered);

            // Assert
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Delivered));
            orderRepoMock.Verify(r => r.UpdateOrderStatusAsync(order), Times.Once);
        }

        [Test]
        public void UpdateOrderStatusAsync_WhenOrderNotFound_ShouldThrowException()
        {
            orderRepoMock.Setup(r => r.GetOrderByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order?)null);

            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await orderService.UpdateOrderStatusAsync(Guid.NewGuid(), OrderStatus.Cancelled));
        }

        [Test]
        public async Task GetUserOrdersAsync_ShouldReturnOrdersInDescendingDateOrder()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            DateTime dateOld = DateTime.Now.AddDays(-1);
            DateTime dateNew = DateTime.Now;

            List<Order> orders = new List<Order>
            {
                new Order { OrderDate = dateOld, Status = OrderStatus.Pending },
                new Order { OrderDate = dateNew, Status = OrderStatus.Delivered }
            };

            orderRepoMock.Setup(r => r.GetUserOrdersAsync(userId)).ReturnsAsync(orders);

            // Act
            List<ViewModels.Order.OrderViewModel> result = (await orderService.GetUserOrdersAsync(userId.ToString())).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result[0].OrderDate, Is.EqualTo(dateNew));
                Assert.That(result[0].Status, Is.EqualTo("Delivered"));

                Assert.That(result[1].OrderDate, Is.EqualTo(dateOld));
                Assert.That(result[1].Status, Is.EqualTo("Pending"));
            });
        }
    }
}