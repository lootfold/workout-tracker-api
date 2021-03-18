using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence
{
    public class LoginRepository : ILoginRepository
    {
        private readonly WorkoutTrackerDbContext dbContext;

        public LoginRepository(WorkoutTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetUserIdByCredentialsAsync(LoginCredentials loginCredentials)
        {
            var loginCredsInDb = await dbContext.LoginCredentials
                .SingleOrDefaultAsync(i => i.Username == loginCredentials.Username
                    && i.Password == loginCredentials.Password);

            return loginCredsInDb != null ? loginCredsInDb.UserId : 0;
        }
    }
}