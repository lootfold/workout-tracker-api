using System.Threading.Tasks;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Business
{
    public class AuthenticationProcessor : IAuthenticationProcessor
    {
        private readonly ILoginCredentialsRepository loginRepository;

        public AuthenticationProcessor(ILoginCredentialsRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        public async Task<int> ValidateCredentialsAsync(LoginCredentials loginCredentials)
        {
            var loginCredsInDb = await this.loginRepository.GetCredentialsAsync(loginCredentials);
            return loginCredsInDb != null ? loginCredsInDb.UserId : 0;
        }
    }
}
