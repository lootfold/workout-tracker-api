using System.Threading.Tasks;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
    }
}
