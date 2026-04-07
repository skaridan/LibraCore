using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services;
using LibraCore.ViewModels.User;
using Moq;

namespace LibraCore.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> userRepoMock = null!;
        private UserService userService = null!;

        [SetUp]
        public void SetUp()
        {
            userRepoMock = new Mock<IUserRepository>();
            userService = new UserService(userRepoMock.Object);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnSortedUsers()
        {
            // Arrange
            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "ZebraUser",
                    Email = "zebra@test.com"
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "AlphaUser",
                    Email = "alpha@test.com"
                }
            };

            userRepoMock.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            List<UserViewModel> result = (await userService.GetAllUsersAsync()).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0].UserName, Is.EqualTo("AlphaUser"));
                Assert.That(result[1].UserName, Is.EqualTo("ZebraUser"));

                Assert.That(result[0].Email, Is.EqualTo("alpha@test.com"));
            });
        }

        [Test]
        public async Task GetAllUsersAsync_WhenEmpty_ShouldReturnEmptyCollection()
        {
            // Arrange
            userRepoMock.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(new List<ApplicationUser>());

            // Act
            IEnumerable<UserViewModel> result = await userService.GetAllUsersAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}