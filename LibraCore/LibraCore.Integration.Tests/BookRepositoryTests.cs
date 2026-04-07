using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Tests.Repositories
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private LibraCoreDbContext dbContext = null!;
        private BookRepository bookRepository = null!;

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
        public async Task GetAllBooksAsync_ShouldReturnOnlyNonDeletedBooks()
        {
            // Arrange
            List<Book> books = new List<Book>
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Active Book",
                    Description = "A valid description for the active book.",
                    IsDeleted = false,
                    AuthorId = Guid.NewGuid(),
                    GenreId = Guid.NewGuid()
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Deleted Book",
                    Description = "A valid description for the deleted book.",
                    IsDeleted = true,
                    AuthorId = Guid.NewGuid(),
                    GenreId = Guid.NewGuid()
                }
            };

            await dbContext.Books.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();

            // Act
            IEnumerable<Book> result = await bookRepository.GetAllBooksAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Title, Is.EqualTo("Active Book"));
                Assert.That(result.Any(b => b.Title == "Deleted Book"), Is.False);
            });
        }

        [Test]
        public async Task GetBookByIdAsync_ShouldReturnNull_IfBookIsDeleted()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book
            {
                Id = bookId,
                Title = "Ghost Book",
                Description = "This book has been soft-deleted from the system.",
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
        public async Task AddBookAsync_ShouldActuallyPersistData()
        {
            // Arrange
            Book book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "New Book",
                Description = "A newly added book with a valid description.",
                AuthorId = Guid.NewGuid(),
                GenreId = Guid.NewGuid()
            };

            // Act
            bool success = await bookRepository.AddBookAsync(book);

            // Assert
            Assert.Multiple(async () =>
            {
                Assert.That(success, Is.True);
                int count = await dbContext.Books.CountAsync();
                Assert.That(count, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task SoftDeleteBookAsync_ShouldChangeIsDeletedProperty()
        {
            // Arrange
            Book book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "To Be Deleted",
                Description = "This book is about to be soft-deleted from the system.",
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

                Book? dbBook = dbContext.Books.IgnoreQueryFilters().FirstOrDefault(b => b.Id == book.Id);
                Assert.That(dbBook, Is.Not.Null);
                Assert.That(dbBook!.IsDeleted, Is.True);
            });
        }

        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrue_WhenBookExistsRegardlessOfStatus()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book book = new Book
            {
                Id = bookId,
                Title = "Exists",
                Description = "This book exists in the database even though it is soft-deleted.",
                IsDeleted = true,
                AuthorId = Guid.NewGuid(),
                GenreId = Guid.NewGuid()
            };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            bool exists = await bookRepository.ExistsByIdAsync(bookId);

            // Assert
            Assert.That(exists, Is.True);
        }
    }
}