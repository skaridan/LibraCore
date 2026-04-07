using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Book;
using LibraCore.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace LibraCore.Tests.Controllers
{
    [TestFixture]
    public class BookControllerTests
    {
        private Mock<IBookService> bookServiceMock = null!;
        private BookController bookController = null!;
        private string userId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            bookServiceMock = new Mock<IBookService>();
            bookController = new BookController(bookServiceMock.Object);

            ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            }, "mock"));

            bookController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Test]
        public async Task Index_ShouldReturnViewWithCorrectPaginationAndSearch()
        {
            // Arrange
            List<BookIndexViewModel> books = new List<BookIndexViewModel>
            {
                new BookIndexViewModel { Title = "C# Advanced" },
                new BookIndexViewModel { Title = "Java Basics" },
                new BookIndexViewModel { Title = "C# Intro" }
            };

            bookServiceMock.Setup(s => s.GetAllBooksOrderedByTitleAsync(userId))
                .ReturnsAsync(books);

            var result = await bookController.Index("C#", 1);

            // Assert
            ViewResult? viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);

            var model = viewResult!.Model as List<BookIndexViewModel>;
            Assert.Multiple(() =>
            {
                Assert.That(model!.Count, Is.EqualTo(2));
                Assert.That(bookController.ViewBag.SearchTerm, Is.EqualTo("C#"));
                Assert.That(bookController.ViewBag.TotalPages, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Index_ShouldHandleInvalidPageNumbers()
        {
            // Arrange
            List<BookIndexViewModel> books = Enumerable.Range(1, 10).Select(i => new BookIndexViewModel { Title = $"Book {i}" }).ToList();
            bookServiceMock.Setup(s => s.GetAllBooksOrderedByTitleAsync(userId)).ReturnsAsync(books);

            // Act
            await bookController.Index(null, 100);

            // Assert
            Assert.That(bookController.ViewBag.CurrentPage, Is.EqualTo(2));
        }

        [Test]
        public async Task Details_WhenBookExists_ShouldReturnView()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            BookDetailsViewModel book = new BookDetailsViewModel { Title = "Existing Book" };
            bookServiceMock.Setup(s => s.GetBookDetailsByIdAsync(bookId)).ReturnsAsync(book);

            // Act
            IActionResult result = await bookController.Details(bookId);

            // Assert
            ViewResult? viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.EqualTo(book));
        }

        [Test]
        public async Task Details_WhenBookDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            bookServiceMock.Setup(s => s.GetBookDetailsByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((BookDetailsViewModel?)null);

            // Act
            IActionResult result = await bookController.Details(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}