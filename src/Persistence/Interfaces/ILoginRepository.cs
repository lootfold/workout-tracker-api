using System.Threading.Tasks;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence.Interfaces
{
    public interface ILoginRepository
    {
        Task<int> GetUserIdByCredentialsAsync(LoginCredentials loginCredentials);
    }
}