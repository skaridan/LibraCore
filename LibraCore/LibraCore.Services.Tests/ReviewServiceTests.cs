using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using LibraCore.ViewModels.Review;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LibraCore.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private Mock<IReviewRepository> reviewRepoMock = null!;
        private ReviewService reviewService = null!;

        [SetUp]
        public void SetUp()
        {
            reviewRepoMock = new Mock<IReviewRepository>();
            reviewService = new ReviewService(reviewRepoMock.Object);
        }

        [Test]
        public async Task GetAllReviewsByBookIdAsync_ShouldReturnCorrectViewModels()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            List<Review> reviews = new List<Review>
            {
                new Review
                {
                    Id = Guid.NewGuid(),
                    BookId = bookId,
                    Book = new Book { Title = "Test Book" },
                    User = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = "User1"
                    },
                    Rating = 5,
                    Comment = "Great!"
                }
            };

            reviewRepoMock.Setup(r => r.GetAllReviewsByBookIdAsync(bookId)).ReturnsAsync(reviews);

            // Act
            List<ReviewViewModel> result = (await reviewService.GetAllReviewsByBookIdAsync(bookId)).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].BookTitle, Is.EqualTo("Test Book"));
                Assert.That(result[0].UserName, Is.EqualTo("User1"));
                Assert.That(result[0].Rating, Is.EqualTo(5));
            });
        }

        [Test]
        public async Task AddReviewAsync_WhenValidData_ShouldSaveReview()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            ReviewInputModel model = new ReviewInputModel
            {
                BookId = bookId,
                Rating = 4,
                Comment = "Very good"
            };

            reviewRepoMock.Setup(r => r.AddReviewAsync(It.IsAny<Review>())).ReturnsAsync(true);

            // Act
            await reviewService.AddReviewAsync(model, userId.ToString());

            // Assert
            reviewRepoMock.Verify(r => r.AddReviewAsync(It.Is<Review>(rev =>
                rev.BookId == bookId &&
                rev.UserId == userId &&
                rev.Rating == 4 &&
                rev.Comment == "Very good")), Times.Once);
        }

        [Test]
        public void AddReviewAsync_WhenRepositoryFails_ShouldThrowPersistException()
        {
            // Arrange
            ReviewInputModel model = new ReviewInputModel { BookId = Guid.NewGuid() };
            reviewRepoMock.Setup(r => r.AddReviewAsync(It.IsAny<Review>())).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () =>
                await reviewService.AddReviewAsync(model, Guid.NewGuid().ToString()));
        }

        [Test]
        public async Task GetReviewByIdAsync_WhenExists_ShouldReturnModel()
        {
            // Arrange
            Guid reviewId = Guid.NewGuid();
            Review review = new Review
            {
                Id = reviewId,
                User = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "Reviewer" 
                },
                Rating = 3,
                Comment = "Average"
            };

            reviewRepoMock.Setup(r => r.GetReviewByIdAsync(reviewId)).ReturnsAsync(review);

            // Act
            ReviewViewModel? result = await reviewService.GetReviewByIdAsync(reviewId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.UserName, Is.EqualTo("Reviewer")); 
            Assert.That(result.Comment, Is.EqualTo("Average"));
        }

        [Test]
        public async Task GetReviewByIdAsync_WhenNotExists_ShouldReturnNull()
        {
            reviewRepoMock.Setup(r => r.GetReviewByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Review?)null);
            ReviewViewModel? result = await reviewService.GetReviewByIdAsync(Guid.NewGuid());
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SoftDeleteReviewAsync_WhenReviewExists_ShouldCallRepository()
        {
            // Arrange
            Guid reviewId = Guid.NewGuid();
            Review review = new Review { Id = reviewId };

            reviewRepoMock.Setup(r => r.GetReviewByIdAsync(reviewId)).ReturnsAsync(review);
            reviewRepoMock.Setup(r => r.SoftDeleteReviewAsync(review)).ReturnsAsync(true);

            // Act
            await reviewService.SoftDeleteReviewAsync(reviewId);

            // Assert
            reviewRepoMock.Verify(r => r.SoftDeleteReviewAsync(review), Times.Once);
        }

        [Test]
        public void SoftDeleteReviewAsync_WhenNotFound_ShouldThrowEntityNotFoundException()
        {
            reviewRepoMock.Setup(r => r.GetReviewByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Review?)null);

            Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await reviewService.SoftDeleteReviewAsync(Guid.NewGuid()));
        }

        [Test]
        public void SoftDeleteReviewAsync_WhenRepositoryFails_ShouldThrowPersistException()
        {
            // Arrange
            Review review = new Review();
            reviewRepoMock.Setup(r => r.GetReviewByIdAsync(It.IsAny<Guid>())).ReturnsAsync(review);
            reviewRepoMock.Setup(r => r.SoftDeleteReviewAsync(review)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<EntityPersistFailureException>(async () =>
                await reviewService.SoftDeleteReviewAsync(Guid.NewGuid()));
        }
    }
}