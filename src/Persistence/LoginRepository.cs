using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Persistence.Interfaces;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence
{
    public class LoginCredentialsRepository : ILoginCredentialsRepository
    {
        private readonly WorkoutTrackerDbContext dbContext;

        public LoginCredentialsRepository(WorkoutTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<LoginCredentials> GetCredentialsAsync(LoginCredentials loginCredentials)
        {
            var loginCredsInDb = await dbContext.LoginCredentials
                .SingleOrDefaultAsync(i => i.Username == loginCredentials.Username
                    && i.Password == loginCredentials.Password);

            return loginCredsInDb;
        }
    }
}
