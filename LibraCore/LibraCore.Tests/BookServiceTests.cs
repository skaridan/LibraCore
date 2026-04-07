using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using LibraCore.ViewModels.Book;
using Moq;
using NUnit.Framework;

namespace LibraCore.Tests
{
    [TestFixture]
    public class BookServiceTests
    {
        // 1. Добави ! или = null!, за да кажеш на компилатора, че ще бъдат инициализирани в SetUp
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
            var author = new Author { Name = "Author" };
            var books = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Zebra", Author = author, Price = 10, IsDeleted = false },
                new Book { Id = Guid.NewGuid(), Title = "Apple", Author = author, Price = 10, IsDeleted = false }
            };

            bookRepoMock.Setup(r => r.GetAllBooksAsync()).ReturnsAsync(books);

            // Act
            var result = (await bookService.GetAllBooksOrderedByTitleAsync(null)).ToList();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Apple"));
            Assert.That(result[1].Title, Is.EqualTo("Zebra"));
        }

        [Test]
        public async Task GetAllBooksOrderedByTitleAsync_WhenUserIdProvided_ShouldMarkFavorites()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();
            var books = new List<Book>
            {
                new Book { Id = bookId, Title = "Fav Book", Author = new Author { Name = "A" }, IsDeleted = false }
            };
            var favorites = new List<UserBook>
            {
                new UserBook { BookId = bookId, UserId = Guid.Parse(userId), IsDeleted = false }
            };

            bookRepoMock.Setup(r => r.GetAllBooksAsync()).ReturnsAsync(books);
            favoriteRepoMock.Setup(r => r.GetAllUserBooksAsync(userId)).ReturnsAsync(favorites);

            // Act
            var result = (await bookService.GetAllBooksOrderedByTitleAsync(userId)).ToList();

            // Assert
            Assert.That(result[0].IsFavorite, Is.True);
        }

        [Test]
        public async Task AddBookAsync_WhenAuthorDoesNotExist_ShouldCreateNewAuthor()
        {
            // Arrange
            var model = new BookInputFormModel
            {
                Title = "T",
                Author = "New Author",
                Price = 10,
                ReleaseDate = DateTime.Now,
                Description = "Some description",
                GenreId = Guid.NewGuid()
            };

            // Важно: Върни null, за да влезе в if(author == null)
            authorRepoMock.Setup(r => r.AuthorExistsByNameAsync("New Author"))
                .ReturnsAsync((Author?)null);

            bookRepoMock.Setup(r => r.AddBookAsync(It.IsAny<Book>()))
                .ReturnsAsync(true);

            // Act
            await bookService.AddBookAsync(model);

            // Assert
            authorRepoMock.Verify(r => r.AddAuthorAsync(It.Is<Author>(a => a.Name == "New Author")), Times.Once);
            bookRepoMock.Verify(r => r.AddBookAsync(It.IsAny<Book>()), Times.Once);
        }
    }
}