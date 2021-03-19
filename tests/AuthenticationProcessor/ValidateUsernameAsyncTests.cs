using Moq;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;
using Xunit;

namespace WorkoutTrackerTests.AuthenticationProcessor
{
    public class ValidateUsernameAsyncTests
    {
        private readonly Mock<IUnitOfWork> uow;
        private readonly Mock<IUserRepository> userRepo;
        private readonly Mock<ILoginCredentialsRepository> loginRepo;
        private readonly IAuthenticationProcessor processor;
        private readonly LoginCredentials loginCredentials = new LoginCredentials()
        {
            Id = 1,
            UserId = 1,
            Username = "lootfold",
            Password = "password"
        };

        public ValidateUsernameAsyncTests()
        {
            uow = new Mock<IUnitOfWork>();
            userRepo = new Mock<IUserRepository>();
            loginRepo = new Mock<ILoginCredentialsRepository>();
            processor = new WorkoutTracker.Business.AuthenticationProcessor(
                uow.Object, loginRepo.Object, userRepo.Object);
        }

        [Fact]
        public async void ShouldFetchLoginCredsFromRepo()
        {
            loginRepo.Setup(l => l.GetLoginCredsByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(loginCredentials);

            var result = await processor.ValidateUsernameAsync(It.IsAny<string>());

            loginRepo.Verify(l => l.GetLoginCredsByUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void ShouldReturnTrueIfLoginCredsNotPresent()
        {
            loginRepo.Setup(l => l.GetLoginCredsByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync((LoginCredentials)null);

            var result = await processor.ValidateUsernameAsync(It.IsAny<string>());

            Assert.True(result);
        }

        [Fact]
        public async void ShouldReturnFalseIfLoginCredsExists()
        {
            loginRepo.Setup(l => l.GetLoginCredsByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(loginCredentials);

            var result = await processor.ValidateUsernameAsync(It.IsAny<string>());

            Assert.False(result);
        }
    }
}
