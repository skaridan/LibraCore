using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework; // ????? ?? [TestFixture], [SetUp] ? Assert

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
            // ???????????? ?? InMemory ???? ????? ? ???????? ??? ?? ????? ????
            var options = new DbContextOptionsBuilder<LibraCoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new LibraCoreDbContext(options);
            bookRepository = new BookRepository(dbContext);

            // ?????????????, ?? ?????? ? ?????? ????? ??????
            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            // ????????? ?????? ? ????????????? ?????????
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        #region GetAllBooksAsync Tests
        [Test]
        public async Task GetAllBooksAsync_ShouldReturnOnlyNonDeletedBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = Guid.NewGuid(), Title = "Active Book", IsDeleted = false, AuthorId = Guid.NewGuid(), GenreId = 1 },
                new Book { Id = Guid.NewGuid(), Title = "Deleted Book", IsDeleted = true, AuthorId = Guid.NewGuid(), GenreId = 1 }
            };

            await dbContext.Books.AddRangeAsync(books);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await bookRepository.GetAllBooksAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Title, Is.EqualTo("Active Book"));
                Assert.That(result.Any(b => b.Title == "Deleted Book"), Is.False);
            });
        }
        #endregion

        #region GetBookByIdAsync Tests
        [Test]
        public async Task GetBookByIdAsync_ShouldReturnNull_IfBookIsDeleted()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Ghost Book", IsDeleted = true, AuthorId = Guid.NewGuid(), GenreId = 1 };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await bookRepository.GetBookByIdAsync(bookId);

            // Assert
            Assert.That(result, Is.Null);
        }
        #endregion

        #region Add/Edit Tests
        [Test]
        public async Task AddBookAsync_ShouldActuallyPersistData()
        {
            // Arrange
            var book = new Book { Id = Guid.NewGuid(), Title = "New Book", AuthorId = Guid.NewGuid(), GenreId = 1 };

            // Act
            var success = await bookRepository.AddBookAsync(book);

            // Assert
            Assert.Multiple(async () =>
            {
                Assert.That(success, Is.True);
                var count = await dbContext.Books.CountAsync();
                Assert.That(count, Is.EqualTo(1));
            });
        }
        #endregion

        #region SoftDelete Tests
        [Test]
        public async Task SoftDeleteBookAsync_ShouldChangeIsDeletedProperty()
        {
            // Arrange
            var book = new Book { Id = Guid.NewGuid(), Title = "To Be Deleted", IsDeleted = false, AuthorId = Guid.NewGuid(), GenreId = 1 };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await bookRepository.SoftDeleteBookAsync(book);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(book.IsDeleted, Is.True);

                // ???????? ???????? ? ?????? ???? DbContext, ??????????? ????????
                var dbBook = dbContext.Books.IgnoreQueryFilters().FirstOrDefault(b => b.Id == book.Id);
                Assert.That(dbBook, Is.Not.Null);
                Assert.That(dbBook!.IsDeleted, Is.True);
            });
        }
        #endregion

        #region Exists Tests
        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrue_WhenBookExistsRegardlessOfStatus()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Exists", IsDeleted = true, AuthorId = Guid.NewGuid(), GenreId = 1 };
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            // Act
            var exists = await bookRepository.ExistsByIdAsync(bookId);

            // Assert
            Assert.That(exists, Is.True);
        }
        #endregion
    }
}