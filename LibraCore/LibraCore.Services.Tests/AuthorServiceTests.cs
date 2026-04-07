using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using LibraCore.ViewModels.Author;
using Moq;

namespace LibraCore.Tests
{
    [TestFixture]
    public class AuthorServiceTests
    {
        private Mock<IAuthorRepository> authorRepoMock = null!;
        private AuthorService authorService = null!;

        [SetUp]
        public void SetUp()
        {
            authorRepoMock = new Mock<IAuthorRepository>();
            authorService = new AuthorService(authorRepoMock.Object);
        }

        [Test]
        public async Task GetAllAuthorsOrderedByNameAsync_ShouldReturnSortedAuthors()
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), Name = "Zhivkov" },
                new Author { Id = Guid.NewGuid(), Name = "Andreev" }
            };

            authorRepoMock.Setup(r => r.GetAllAuthorsAsync()).ReturnsAsync(authors);

            // Act
            List<AuthorViewModel> result = (await authorService.GetAllAuthorsOrderedByNameAsync()).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0].Name, Is.EqualTo("Andreev"));
                Assert.That(result[1].Name, Is.EqualTo("Zhivkov"));
            });
        }

        [Test]
        public async Task AddAuthorAsync_WhenAuthorIsNew_ShouldSaveSuccessfully()
        {
            // Arrange
            AuthorInputModel model = new AuthorInputModel { Name = "New Author" };

            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync(model.Name))
                .ReturnsAsync((Author?)null);

            authorRepoMock.Setup(r => r.AddAuthorAsync(It.IsAny<Author>()))
                .ReturnsAsync(true);

            // Act
            await authorService.AddAuthorAsync(model);

            // Assert
            authorRepoMock.Verify(r => r.AddAuthorAsync(It.Is<Author>(a => a.Name == model.Name)), Times.Once);
        }

        [Test]
        public void AddAuthorAsync_WhenAuthorAlreadyExists_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            AuthorInputModel model = new AuthorInputModel { Name = "Existing Author" };
            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync(model.Name))
                .ReturnsAsync(new Author { Name = model.Name });

            // Act & Assert
            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
                await authorService.AddAuthorAsync(model));
        }

        [Test]
        public void AddAuthorAsync_WhenRepositoryFailsToSave_ShouldThrowEntityPersistFailureException()
        {
            // Arrange
            AuthorInputModel model = new AuthorInputModel { Name = "Valid Name" };
            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync(model.Name)).ReturnsAsync((Author?)null);
            authorRepoMock.Setup(r => r.AddAuthorAsync(It.IsAny<Author>())).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () =>
                await authorService.AddAuthorAsync(model));
        }

        [Test]
        public async Task GetAuthorByIdAsync_WhenAuthorExists_ShouldReturnCorrectViewModel()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            Author author = new Author { Id = authorId, Name = "Test Author" };
            authorRepoMock.Setup(r => r.GetAuthorByIdAsync(authorId)).ReturnsAsync(author);

            // Act
            AuthorViewModel? result = await authorService.GetAuthorByIdAsync(authorId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(authorId));
            Assert.That(result.Name, Is.EqualTo("Test Author"));
        }

        [Test]
        public async Task GetAuthorByIdAsync_WhenAuthorDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            authorRepoMock.Setup(r => r.GetAuthorByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Author?)null);

            // Act
            AuthorViewModel? result = await authorService.GetAuthorByIdAsync(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SoftDeleteAuthorAsync_WhenAuthorExists_ShouldCallRepository()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            Author author = new Author { Id = authorId, Name = "To Be Deleted" };

            authorRepoMock.Setup(r => r.GetAuthorByIdAsync(authorId)).ReturnsAsync(author);
            authorRepoMock.Setup(r => r.SoftDeleteAuthorAsync(author)).ReturnsAsync(true);

            // Act
            await authorService.SoftDeleteAuthorAsync(authorId);

            // Assert
            authorRepoMock.Verify(r => r.SoftDeleteAuthorAsync(author), Times.Once);
        }

        [Test]
        public void SoftDeleteAuthorAsync_WhenAuthorNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            authorRepoMock.Setup(r => r.GetAuthorByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Author?)null);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await authorService.SoftDeleteAuthorAsync(Guid.NewGuid()));
        }

        [Test]
        public void SoftDeleteAuthorAsync_WhenRepositoryFails_ShouldThrowEntityPersistFailureException()
        {
            // Arrange
            Author author = new Author { Id = Guid.NewGuid() };
            authorRepoMock.Setup(r => r.GetAuthorByIdAsync(author.Id)).ReturnsAsync(author);
            authorRepoMock.Setup(r => r.SoftDeleteAuthorAsync(author)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () =>
                await authorService.SoftDeleteAuthorAsync(author.Id));
        }
    }
}