using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using Moq;

namespace LibraCore.Tests
{
    [TestFixture]
    public class FavoriteServiceTests
    {
        private Mock<IFavoriteRepository> favoriteRepoMock = null!;
        private Mock<IBookRepository> bookRepoMock = null!;
        private FavoriteService favoriteService = null!;

        [SetUp]
        public void SetUp()
        {
            favoriteRepoMock = new Mock<IFavoriteRepository>();
            bookRepoMock = new Mock<IBookRepository>();
            favoriteService = new FavoriteService(favoriteRepoMock.Object, bookRepoMock.Object);
        }

        [Test]
        public async Task GetUserBooksOrderedByTitleAsync_ShouldReturnSortedFavoriteBooks()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Author author = new Author { Name = "Author" };
            List<UserBook> userBooks = new List<UserBook>
            {
                new UserBook { Book = new Book { Title = "B", Author = author, Price = 10 } },
                new UserBook { Book = new Book { Title = "A", Author = author, Price = 20 } }
            };

            favoriteRepoMock.Setup(r => r.GetAllUserBooksAsync(userId)).ReturnsAsync(userBooks);

            // Act
            List<ViewModels.Favorite.FavoriteViewModel> result = (await favoriteService.GetUserBooksOrderedByTitleAsync(userId)).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0].Title, Is.EqualTo("A"));
                Assert.That(result[1].Title, Is.EqualTo("B"));
            });
        }

        [Test]
        public async Task AddToFavoritesAsync_WhenNew_ShouldCreateUserBook()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();

            favoriteRepoMock.Setup(r => r.GetUserBookAsync(userId, bookId)).ReturnsAsync((UserBook?)null);
            bookRepoMock.Setup(r => r.ExistsByIdAsync(bookId)).ReturnsAsync(true);
            favoriteRepoMock.Setup(r => r.AddUserBookAsync(It.IsAny<UserBook>())).ReturnsAsync(true);

            // Act
            await favoriteService.AddToFavoritesAsync(userId, bookId);

            // Assert
            favoriteRepoMock.Verify(r => r.AddUserBookAsync(It.Is<UserBook>(ub =>
                ub.UserId == Guid.Parse(userId) && ub.BookId == bookId)), Times.Once);
        }

        [Test]
        public async Task AddToFavoritesAsync_WhenPreviouslyDeleted_ShouldUpdateIsDeletedToFalse()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();
            UserBook existingDeletedBook = new UserBook { UserId = Guid.Parse(userId), BookId = bookId, IsDeleted = true };

            favoriteRepoMock.Setup(r => r.GetUserBookAsync(userId, bookId)).ReturnsAsync(existingDeletedBook);
            bookRepoMock.Setup(r => r.ExistsByIdAsync(bookId)).ReturnsAsync(true);
            favoriteRepoMock.Setup(r => r.UpdateUserBookAsync(existingDeletedBook)).ReturnsAsync(true);

            // Act
            await favoriteService.AddToFavoritesAsync(userId, bookId);

            // Assert
            Assert.That(existingDeletedBook.IsDeleted, Is.False);
            favoriteRepoMock.Verify(r => r.UpdateUserBookAsync(existingDeletedBook), Times.Once);
        }

        [Test]
        public void AddToFavoritesAsync_WhenAlreadyExistsAndNotDeleted_ShouldThrowException()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();
            UserBook activeFavorite = new UserBook { IsDeleted = false };

            favoriteRepoMock.Setup(r => r.GetUserBookAsync(userId, bookId)).ReturnsAsync(activeFavorite);

            // Act & Assert
            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
                await favoriteService.AddToFavoritesAsync(userId, bookId));
        }

        [Test]
        public void AddToFavoritesAsync_WhenBookDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();

            favoriteRepoMock.Setup(r => r.GetUserBookAsync(userId, bookId)).ReturnsAsync((UserBook?)null);
            bookRepoMock.Setup(r => r.ExistsByIdAsync(bookId)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await favoriteService.AddToFavoritesAsync(userId, bookId));
        }

        [Test]
        public async Task RemoveBookFromFavoritesAsync_WhenExists_ShouldCallSoftDelete()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();
            Guid bookId = Guid.NewGuid();
            UserBook userBook = new UserBook();

            favoriteRepoMock.Setup(r => r.GetUserBookAsync(userId, bookId)).ReturnsAsync(userBook);
            favoriteRepoMock.Setup(r => r.SoftDeleteUserBookAsync(userBook)).ReturnsAsync(true);

            // Act
            await favoriteService.RemoveBookFromFavoritesAsync(userId, bookId);

            // Assert
            favoriteRepoMock.Verify(r => r.SoftDeleteUserBookAsync(userBook), Times.Once);
        }

        [Test]
        public void RemoveBookFromFavoritesAsync_WhenNotFound_ShouldThrowException()
        {
            // Arrange
            favoriteRepoMock.Setup(r => r.GetUserBookAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync((UserBook?)null);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await favoriteService.RemoveBookFromFavoritesAsync("user", Guid.NewGuid()));
        }

        [Test]
        public void RemoveBookFromFavoritesAsync_WhenRepositoryFails_ShouldThrowPersistException()
        {
            // Arrange
            UserBook userBook = new UserBook();
            favoriteRepoMock.Setup(r => r.GetUserBookAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(userBook);
            favoriteRepoMock.Setup(r => r.SoftDeleteUserBookAsync(userBook)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () =>
                await favoriteService.RemoveBookFromFavoritesAsync("user", Guid.NewGuid()));
        }
    }
}