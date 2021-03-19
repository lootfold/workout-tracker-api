using System;
using Moq;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;
using Xunit;

namespace WorkoutTrackerTests.AuthenticationProcessor
{
    public class SignUpAsyncTests
    {
        private readonly Mock<IUnitOfWork> uow;
        private readonly Mock<IUserRepository> userRepo;
        private readonly Mock<ILoginCredentialsRepository> loginRepo;
        private readonly IAuthenticationProcessor processor;

        private readonly LoginCredentials loginCredentials = new LoginCredentials()
        {
            Username = "lootfold",
            Password = "password"
        };

        private readonly User user = new User()
        {
            Name = "Pallav"
        };

        public SignUpAsyncTests()
        {
            uow = new Mock<IUnitOfWork>();
            userRepo = new Mock<IUserRepository>();

            loginRepo = new Mock<ILoginCredentialsRepository>();

            userRepo.Setup(u => u.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync(new User() { Id = 1, Name = "Test User" });

            processor = new WorkoutTracker.Business.AuthenticationProcessor(
                uow.Object, loginRepo.Object, userRepo.Object);
        }

        [Fact]
        public async void ShouldCreateSqlTransaction()
        {
            var result = await processor.SignUpAsync(user, loginCredentials);

            uow.Verify(u => u.BeginTransactionAsync(), Times.Once);
            uow.Verify(u => u.SaveAsync(), Times.Exactly(2));
            uow.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void ShouldAddUserAndCredsToTheDb()
        {
            var result = await processor.SignUpAsync(user, loginCredentials);

            userRepo.Verify(u => u.AddUserAsync(It.IsAny<User>()), Times.Once);
            loginRepo.Verify(l => l.AddLoginCredentials(It.IsAny<LoginCredentials>()), Times.Once);
        }

        [Fact]
        public async void ShouldReturnNewUserOnSuccess()
        {
            var result = await processor.SignUpAsync(user, loginCredentials);

            Assert.Equal(1, result.Id);
            Assert.Equal("Test User", result.Name);
        }

        [Fact]
        public async void ShouldNotCommitOnError()
        {
            uow.Setup(u => u.SaveAsync()).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => processor.SignUpAsync(user, loginCredentials));
            uow.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}