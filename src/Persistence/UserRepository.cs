using System.Threading.Tasks;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkoutTrackerDbContext dbContext;

        public UserRepository(WorkoutTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            return user;
        }
    }
}
