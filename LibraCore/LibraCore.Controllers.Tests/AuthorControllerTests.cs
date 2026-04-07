using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Author;
using LibraCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraCore.Tests.Controllers
{
    [TestFixture]
    public class AuthorControllerTests
    {
        private Mock<IAuthorService> authorServiceMock = null!;
        private AuthorController authorController = null!;

        [SetUp]
        public void SetUp()
        {
            authorServiceMock = new Mock<IAuthorService>();

            authorController = new AuthorController(authorServiceMock.Object);
        }

        [Test]
        public async Task Index_ShouldReturnViewWithCorrectModel()
        {
            // Arrange
            List<AuthorViewModel> authors = new List<AuthorViewModel>
            {
                new AuthorViewModel { Id = Guid.NewGuid(), Name = "Ivan Vazov" },
                new AuthorViewModel { Id = Guid.NewGuid(), Name = "Jordan Jovkov" }
            };

            authorServiceMock
                .Setup(s => s.GetAllAuthorsOrderedByNameAsync())
                .ReturnsAsync(authors);

            // Act
            IActionResult result = await authorController.Index();

            // Assert
            ViewResult? viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Result should be a ViewResult");

            IEnumerable<AuthorViewModel>? model = viewResult!.Model as IEnumerable<AuthorViewModel>;
            Assert.Multiple(() =>
            {
                Assert.That(model, Is.Not.Null, "Model should not be null");
                Assert.That(model!.Count(), Is.EqualTo(2), "Model should contain exactly 2 authors");
                Assert.That(model!.First().Name, Is.EqualTo("Ivan Vazov"));
            });
        }

        [Test]
        public async Task Index_ShouldReturnEmptyModel_WhenNoAuthorsExist()
        {
            // Arrange
            authorServiceMock
                .Setup(s => s.GetAllAuthorsOrderedByNameAsync())
                .ReturnsAsync(new List<AuthorViewModel>());

            // Act
            IActionResult result = await authorController.Index();

            // Assert
            ViewResult? viewResult = result as ViewResult;
            IEnumerable<AuthorViewModel>? model = viewResult!.Model as IEnumerable<AuthorViewModel>;

            Assert.That(model, Is.Empty);
        }
    }
}