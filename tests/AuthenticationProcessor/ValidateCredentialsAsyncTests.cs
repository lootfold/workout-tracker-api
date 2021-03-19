using Moq;
using WorkoutTracker.Business;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;
using Xunit;

namespace WorkoutTrackerTests.AuthenticationProcessor
{
    public class ValidateCredentialsAsyncTests
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

        public ValidateCredentialsAsyncTests()
        {
            uow = new Mock<IUnitOfWork>();
            userRepo = new Mock<IUserRepository>();
            loginRepo = new Mock<ILoginCredentialsRepository>();
            processor = new WorkoutTracker.Business.AuthenticationProcessor(
                uow.Object, loginRepo.Object, userRepo.Object);
        }

        [Fact]
        public async void ShouldReturnUserIdForValidCreds()
        {
            loginRepo.Setup(r => r.GetCredentialsAsync(It.IsAny<LoginCredentials>()))
                .ReturnsAsync(loginCredentials);

            var result = await processor.ValidateCredentialsAsync(loginCredentials);

            Assert.Equal<int>(loginCredentials.Id, result);
        }

        [Fact]
        public async void ShouldReturnZeroForInvalidCreds()
        {
            loginRepo.Setup(l => l.GetCredentialsAsync(It.IsAny<LoginCredentials>()))
                .ReturnsAsync((LoginCredentials)null);

            var result = await processor.ValidateCredentialsAsync(loginCredentials);

            Assert.Equal<int>(0, result);
        }

        [Fact]
        public async void ShouldCallGetCredentialsAsyncOnValidateCreds()
        {
            loginRepo.Setup(l => l.GetCredentialsAsync(It.IsAny<LoginCredentials>()))
                .ReturnsAsync((LoginCredentials)null);

            var result = await processor.ValidateCredentialsAsync(loginCredentials);

            loginRepo.Verify(l => l.GetCredentialsAsync(It.IsAny<LoginCredentials>()), Times.Once);
        }
    }
}