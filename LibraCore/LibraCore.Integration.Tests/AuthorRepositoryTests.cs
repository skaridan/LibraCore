using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Tests.Repositories
{
    [TestFixture]
    public class AuthorRepositoryTests
    {
        private LibraCoreDbContext dbContext;
        private AuthorRepository authorRepository;

        [SetUp]
        public void SetUp()
        {
            DbContextOptions<LibraCoreDbContext> options = new DbContextOptionsBuilder<LibraCoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new LibraCoreDbContext(options);
            authorRepository = new AuthorRepository(dbContext);

            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllAuthorsAsync_ShouldReturnOnlyNonDeletedAuthors()
        {
            // Arrange
            List<Author> authors = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), Name = "Active Author", IsDeleted = false },
                new Author { Id = Guid.NewGuid(), Name = "Deleted Author", IsDeleted = true }
            };
            await dbContext.Authors.AddRangeAsync(authors);
            await dbContext.SaveChangesAsync();

            // Act
            IEnumerable<Author> result = await authorRepository.GetAllAuthorsAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Name, Is.EqualTo("Active Author"));
            });
        }

        [Test]
        public async Task AuthorExistsByNameAsync_ShouldBeCaseInsensitive_AndReturnAuthor()
        {
            // Arrange
            string authorName = "Stephen King";
            Author author = new Author { Id = Guid.NewGuid(), Name = authorName, IsDeleted = false };
            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();

            // Act
            Author? result = await authorRepository.AuthorExistsByNameAsync("stephen king");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo(authorName));
        }

        [Test]
        public async Task GetAuthorByIdAsync_ShouldReturnNull_IfAuthorIsDeleted()
        {
            // Arrange
            Guid authorId = Guid.NewGuid();
            Author author = new Author { Id = authorId, Name = "Ghost", IsDeleted = true };
            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();

            // Act
            Author? result = await authorRepository.GetAuthorByIdAsync(authorId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddAuthorAsync_ShouldReturnTrue_WhenSuccessfullyAdded()
        {
            // Arrange
            Author author = new Author { Id = Guid.NewGuid(), Name = "New Author" };

            // Act
            bool success = await authorRepository.AddAuthorAsync(author);

            // Assert
            Assert.Multiple(async () =>
            {
                Assert.That(success, Is.True);
                Assert.That(await dbContext.Authors.CountAsync(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task SoftDeleteAuthorAsync_ShouldUpdateIsDeletedProperty()
        {
            // Arrange
            Author author = new Author { Id = Guid.NewGuid(), Name = "To Be Deleted", IsDeleted = false };
            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();

            // Act
            bool result = await authorRepository.SoftDeleteAuthorAsync(author);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(author.IsDeleted, Is.True);

                Author? dbAuthor = dbContext.Authors.IgnoreQueryFilters().FirstOrDefault(a => a.Id == author.Id);
                Assert.That(dbAuthor!.IsDeleted, Is.True);
            });
        }
    }
}