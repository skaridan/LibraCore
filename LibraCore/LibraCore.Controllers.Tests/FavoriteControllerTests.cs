using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Favorite;
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
    public class FavoriteControllerTests
    {
        private Mock<IFavoriteService> favoriteServiceMock = null!;
        private Mock<ILogger<FavoriteController>> loggerMock = null!;
        private FavoriteController favoriteController = null!;
        private ITempDataDictionary tempData = null!;
        private string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            favoriteServiceMock = new Mock<IFavoriteService>();
            loggerMock = new Mock<ILogger<FavoriteController>>();
            favoriteController = new FavoriteController(favoriteServiceMock.Object, loggerMock.Object);

            ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            }, "mock"));

            Mock<ITempDataProvider> tempDataProvider = new Mock<ITempDataProvider>();
            tempData = new TempDataDictionary(new DefaultHttpContext(), tempDataProvider.Object);

            favoriteController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user },
            };
            favoriteController.TempData = tempData;
        }

        [Test]
        public async Task Index_ShouldReturnViewWithFavorites()
        {
            // Arrange
            List<FavoriteViewModel> favorites = new List<FavoriteViewModel> { new FavoriteViewModel { Title = "Fav Book" } };
            favoriteServiceMock.Setup(s => s.GetUserBooksOrderedByTitleAsync(userId)).ReturnsAsync(favorites);

            // Act
            IActionResult result = await favoriteController.Index();

            // Assert
            ViewResult? viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.EqualTo(favorites));
        }

        [Test]
        public async Task Add_WhenSuccessful_ShouldRedirectToBookIndex()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            // Act
            var result = await favoriteController.Add(bookId);

            // Assert
            RedirectToActionResult? redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
            Assert.That(redirect.ControllerName, Is.EqualTo("Book"));
            Assert.That(tempData["Success"], Is.Not.Null);
        }

        [Test]
        public async Task Add_WhenAlreadyExists_ShouldReturnBadRequest()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            favoriteServiceMock.Setup(s => s.AddToFavoritesAsync(userId, bookId))
                .ThrowsAsync(new EntityAlreadyExistsException());

            // Act
            IActionResult result = await favoriteController.Add(bookId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
            Assert.That(tempData["Error"], Is.Not.Null);
        }

        [Test]
        public async Task Add_WhenBookNotFound_ShouldReturnNotFound()
        {
            // Arrange
            favoriteServiceMock.Setup(s => s.AddToFavoritesAsync(userId, It.IsAny<Guid>()))
                .ThrowsAsync(new EntityNotFoundException());

            // Act
            IActionResult result = await favoriteController.Add(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Remove_WhenSuccessful_ShouldRedirectToIndex()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();

            // Act
            IActionResult result = await favoriteController.Remove(bookId);

            // Assert
            RedirectToActionResult? redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
            favoriteServiceMock.Verify(s => s.RemoveBookFromFavoritesAsync(userId, bookId), Times.Once);
        }

        [Test]
        public async Task Remove_WhenPersistFails_ShouldRedirectToIndexWithErrorMessage()
        {
            // Arrange
            favoriteServiceMock.Setup(s => s.RemoveBookFromFavoritesAsync(userId, It.IsAny<Guid>()))
                .ThrowsAsync(new EntityPersistFailureException());

            // Act
            IActionResult result = await favoriteController.Remove(Guid.NewGuid());

            // Assert
            RedirectToActionResult? redirect = result as RedirectToActionResult;
            Assert.That(redirect!.ActionName, Is.EqualTo("Index"));
            loggerMock.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()), Times.Once);
        }
    }
}