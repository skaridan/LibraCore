using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Order;
using LibraCore.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace LibraCore.Tests.Controllers
{
    [TestFixture]
    public class OrderControllerTests
    {
        private Mock<IOrderService> orderServiceMock = null!;
        private Mock<ILogger<OrderController>> loggerMock = null!;
        private OrderController orderController = null!;
        private ITempDataDictionary tempData = null!;
        private string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            orderServiceMock = new Mock<IOrderService>();
            loggerMock = new Mock<ILogger<OrderController>>();
            orderController = new OrderController(orderServiceMock.Object, loggerMock.Object);

            ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            }, "mock"));

            Mock<ITempDataProvider> tempDataProvider = new Mock<ITempDataProvider>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), tempDataProvider.Object);

            orderController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            orderController.TempData = tempData;
        }

        [Test]
        public async Task Index_ShouldReturnViewWithUserOrders()
        {
            // Arrange
            List<OrderViewModel> orders = new List<OrderViewModel> { new OrderViewModel { Id = Guid.NewGuid() } };
            orderServiceMock.Setup(s => s.GetUserOrdersAsync(userId)).ReturnsAsync(orders);

            // Act
            IActionResult result = await orderController.Index();

            // Assert
            ViewResult? viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.EqualTo(orders));
        }

        [Test]
        public async Task Details_WithEmptyGuid_ShouldReturnBadRequest()
        {
            // Act
            IActionResult result = await orderController.Details(Guid.Empty);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Details_WhenOrderNotFound_ShouldReturnNotFound()
        {
            // Arrange
            orderServiceMock.Setup(s => s.GetOrderDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync((OrderDetailsViewModel?)null);

            // Act
            IActionResult result = await orderController.Details(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Buy_WithEmptyGuid_ShouldReturnBadRequest()
        {
            // Act
            IActionResult result = await orderController.Buy(Guid.Empty);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task Buy_WhenSuccessful_ShouldSetSuccessTempDataAndRedirect()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();

            // Act
            IActionResult result = await orderController.Buy(bookId);

            // Assert
            RedirectToActionResult? redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo(nameof(OrderController.Index)));
            Assert.That(tempData["Success"], Is.Not.Null);
            orderServiceMock.Verify(s => s.CreateOrderAsync(bookId, userId), Times.Once);
        }

        [Test]
        public async Task Buy_WhenBookNotFound_ShouldReturnNotFoundAndSetErrorTempData()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            orderServiceMock.Setup(s => s.CreateOrderAsync(bookId, userId))
                .ThrowsAsync(new EntityNotFoundException());

            // Act
            IActionResult result = await orderController.Buy(bookId);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            Assert.That(tempData["Error"], Is.Not.Null);

            loggerMock.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
        }

        [Test]
        public async Task Buy_WhenPersistFails_ShouldRedirectToIndexWithError()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            orderServiceMock.Setup(s => s.CreateOrderAsync(bookId, userId))
                .ThrowsAsync(new EntityPersistFailureException());

            // Act
            IActionResult result = await orderController.Buy(bookId);

            // Assert
            RedirectToActionResult? redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo(nameof(OrderController.Index)));
            Assert.That(tempData["Error"], Is.Not.Null);
        }
    }
}