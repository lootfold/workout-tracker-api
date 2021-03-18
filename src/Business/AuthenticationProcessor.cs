using System.Threading.Tasks;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Business
{
    public class AuthenticationProcessor : IAuthenticationProcessor
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoginCredentialsRepository loginRepository;
        private readonly IUserRepository userRepository;

        public AuthenticationProcessor(
            IUnitOfWork unitOfWork,
            ILoginCredentialsRepository loginRepository,
            IUserRepository userRepository)
        {
            this.loginRepository = loginRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> ValidateCredentialsAsync(LoginCredentials loginCredentials)
        {
            var loginCredsInDb = await this.loginRepository.GetCredentialsAsync(loginCredentials);
            return loginCredsInDb != null ? loginCredsInDb.UserId : 0;
        }
        public async Task<User> SignUpAsync(User user, LoginCredentials loginCredentials)
        {
            await unitOfWork.BeginTransactionAsync();

            var newUser = await userRepository.AddUserAsync(user);
            await unitOfWork.SaveAsync();

            loginCredentials.UserId = newUser.Id;
            await loginRepository.AddLoginCredentials(loginCredentials);
            await unitOfWork.SaveAsync();

            await unitOfWork.CommitAsync();

            return newUser;
        }
    }
}
