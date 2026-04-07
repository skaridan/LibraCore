using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using LibraCore.ViewModels.Book;
using Moq;

namespace LibraCore.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        private Mock<IBookRepository> bookRepoMock = null!;
        private Mock<IAuthorRepository> authorRepoMock = null!;
        private Mock<IFavoriteRepository> favoriteRepoMock = null!;
        private BookService bookService = null!;

        [SetUp]
        public void SetUp()
        {
            bookRepoMock = new Mock<IBookRepository>();
            authorRepoMock = new Mock<IAuthorRepository>();
            favoriteRepoMock = new Mock<IFavoriteRepository>();

            bookService = new BookService(
                bookRepoMock.Object,
                authorRepoMock.Object,
                favoriteRepoMock.Object);
        }

        [Test]
        public async Task GetAllBooksOrderedByTitleAsync_ShouldReturnSortedBooks()
        {
            // Arrange
            Author author = new Author { Name = "Author Name" };
            List<Book> books = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Zebra", Author = author, Price = 10 },
                new Book { Id = Guid.NewGuid(), Title = "Apple", Author = author, Price = 15 }
            };

            bookRepoMock.Setup(r => r.GetAllBooksAsync()).ReturnsAsync(books);

            // Act
            List<BookIndexViewModel> result = (await bookService.GetAllBooksOrderedByTitleAsync(null)).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0].Title, Is.EqualTo("Apple"));
                Assert.That(result[1].Title, Is.EqualTo("Zebra"));
            });
        }

        [Test]
        public async Task GetAllBooksOrderedByTitleAsync_WithUserId_ShouldMarkFavorites()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();
            List<Book> books = new List<Book>
            {
                new Book { Id = bookId, Title = "Title", Author = new Author { Name = "A" } }
            };
            List<UserBook> favorites = new List<UserBook>
            {
                new UserBook { BookId = bookId, UserId = Guid.Parse(userId) }
            };

            bookRepoMock.Setup(r => r.GetAllBooksAsync()).ReturnsAsync(books);
            favoriteRepoMock.Setup(r => r.GetAllUserBooksAsync(userId)).ReturnsAsync(favorites);

            // Act
            List<BookIndexViewModel> result = (await bookService.GetAllBooksOrderedByTitleAsync(userId)).ToList();

            // Assert
            Assert.That(result[0].IsFavorite, Is.True);
        }

        [Test]
        public async Task AddBookAsync_WhenAuthorDoesNotExist_ShouldCreateNewAuthor()
        {
            // Arrange
            BookInputFormModel model = new BookInputFormModel
            {
                Title = "New Book",
                Author = "New Author",
                Price = 10,
                ReleaseDate = DateTime.Now
            };

            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync(model.Author)).ReturnsAsync((Author?)null);
            bookRepoMock.Setup(r => r.AddBookAsync(It.IsAny<Book>())).ReturnsAsync(true);

            // Act
            await bookService.AddBookAsync(model);

            // Assert
            authorRepoMock.Verify(r => r.AddAuthorAsync(It.Is<Author>(a => a.Name == "New Author")), Times.Once);
            bookRepoMock.Verify(r => r.AddBookAsync(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        public void AddBookAsync_WhenRepositoryFails_ShouldThrowEntityPersistFailureException()
        {
            // Arrange
            BookInputFormModel model = new BookInputFormModel { Title = "T", Author = "A", ReleaseDate = DateTime.Now };
            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync(It.IsAny<string>())).ReturnsAsync(new Author());
            bookRepoMock.Setup(r => r.AddBookAsync(It.IsAny<Book>())).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () => await bookService.AddBookAsync(model));
        }

        [Test]
        public async Task GetBookDetailsByIdAsync_WhenBookExists_ShouldReturnModel()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book
            {
                Id = bookId,
                Title = "Test",
                Author = new Author { Name = "A" },
                Genre = new Genre { Name = "G" },
                ReleaseDate = new DateOnly(2020, 1, 1)
            };
            bookRepoMock.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);

            // Act
            BookDetailsViewModel? result = await bookService.GetBookDetailsByIdAsync(bookId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Test"));
        }

        [Test]
        public async Task GetBookDetailsByIdAsync_WhenBookDoesNotExist_ShouldReturnNull()
        {
            bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Book?)null);
            BookDetailsViewModel? result = await bookService.GetBookDetailsByIdAsync(Guid.NewGuid());
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task EditBookAsync_WhenBookExists_ShouldUpdateProperties()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book existingBook = new Book { Id = bookId, Title = "Old" };
            BookInputFormModel model = new BookInputFormModel { Title = "New", Author = "A", ReleaseDate = DateTime.Now };

            bookRepoMock.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(existingBook);
            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync(It.IsAny<string>())).ReturnsAsync(new Author { Id = Guid.NewGuid() });
            bookRepoMock.Setup(r => r.EditBookAsync(existingBook)).ReturnsAsync(true);

            // Act
            await bookService.EditBookAsync(bookId, model);

            // Assert
            Assert.That(existingBook.Title, Is.EqualTo("New"));
            bookRepoMock.Verify(r => r.EditBookAsync(existingBook), Times.Once);
        }

        [Test]
        public void EditBookAsync_WhenBookNotFound_ShouldThrowEntityNotFoundException()
        {
            bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Book?)null);
            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await bookService.EditBookAsync(Guid.NewGuid(), new BookInputFormModel()));
        }

        [Test]
        public async Task SoftDeleteBookAsync_WhenBookExists_ShouldCallRepository()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book { Id = bookId };
            bookRepoMock.Setup(r => r.GetBookByIdAsync(bookId)).ReturnsAsync(book);
            bookRepoMock.Setup(r => r.SoftDeleteBookAsync(book)).ReturnsAsync(true);

            // Act
            await bookService.SoftDeleteBookAsync(bookId);

            // Assert
            bookRepoMock.Verify(r => r.SoftDeleteBookAsync(book), Times.Once);
        }

        [Test]
        public void SoftDeleteBookAsync_WhenRepositoryFails_ShouldThrowPersistException()
        {
            // Arrange
            Book book = new Book();
            bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<Guid>())).ReturnsAsync(book);
            bookRepoMock.Setup(r => r.SoftDeleteBookAsync(book)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () => await bookService.SoftDeleteBookAsync(Guid.NewGuid()));
        }
    }
}