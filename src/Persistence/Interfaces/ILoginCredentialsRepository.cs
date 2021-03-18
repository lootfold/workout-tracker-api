using System.Threading.Tasks;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence.Interfaces
{
    public interface ILoginCredentialsRepository
    {
        Task<LoginCredentials> GetCredentialsAsync(LoginCredentials loginCredentials);

        Task AddLoginCredentials(LoginCredentials loginCredentials);
    }
}
