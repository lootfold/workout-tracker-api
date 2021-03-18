using System.Threading.Tasks;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Business.Interfacs
{
    public interface IAuthenticationProcessor
    {
        Task<int> ValidateCredentialsAsync(LoginCredentials loginCredentials);

        Task<User> SignUpAsync(User user, LoginCredentials loginCredentials);
    }
}
