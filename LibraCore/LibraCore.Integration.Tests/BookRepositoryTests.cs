using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Tests.Repositories
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private LibraCoreDbContext dbContext;
        private BookRepository bookRepository;

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<LibraCoreDbContext> options = new DbContextOptionsBuilder<LibraCoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new LibraCoreDbContext(options);
            bookRepository = new BookRepository(dbContext);

            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllBooksAsync_ShouldReturnOnlyActiveBooks()
        {
            // Arrange
            List<Book> books = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Active", IsDeleted = false, AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() },
                new Book { Id = Guid.NewGuid(), Title = "Deleted", IsDeleted = true, AuthorId = Guid.NewGuid(), GenreId = Guid.NewGuid() }
            };
            await dbContext.Books.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();

            // Act
            IEnumerable<Book> result = await bookRepository.GetAllBooksAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Title, Is.EqualTo("Active"));
            });
        }

        [Test]
        public async Task GetBookByIdAsync_ShouldReturnNull_WhenBookIsSoftDeleted()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book
            {
                Id = bookId,
                Title = "Test",
                IsDeleted = true,
                AuthorId = Guid.NewGuid(),
                GenreId = Guid.NewGuid()
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            Book? result = await bookRepository.GetBookByIdAsync(bookId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddBookAsync_ShouldSaveBookToDatabase()
        {
            // Arrange
            Book book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "New Book",
                AuthorId = Guid.NewGuid(),
                GenreId = Guid.NewGuid() 
            };

            // Act
            bool success = await bookRepository.AddBookAsync(book);

            // Assert
            Assert.Multiple(async () =>
            {
                Assert.That(success, Is.True);
                Assert.That(await dbContext.Books.CountAsync(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task SoftDeleteBookAsync_ShouldSetFlagToTrue()
        {
            // Arrange
            Book book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "To Delete",
                IsDeleted = false,
                AuthorId = Guid.NewGuid(),
                GenreId = Guid.NewGuid()
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            bool result = await bookRepository.SoftDeleteBookAsync(book);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(book.IsDeleted, Is.True);

                var dbBook = dbContext.Books.IgnoreQueryFilters().FirstOrDefault(b => b.Id == book.Id);
                Assert.That(dbBook, Is.Not.Null);
                Assert.That(dbBook!.IsDeleted, Is.True);
            });
        }

        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrue_EvenIfDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            await dbContext.Books.AddAsync(new Book
            {
                Id = id,
                Title = "Exists",
                IsDeleted = true,
                AuthorId = Guid.NewGuid(),
                GenreId = Guid.NewGuid() 
            });
            await dbContext.SaveChangesAsync();

            // Act
            bool exists = await bookRepository.ExistsByIdAsync(id);

            // Assert
            Assert.That(exists, Is.True);
        }
    }
}