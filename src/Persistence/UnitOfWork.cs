using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace WorkoutTracker.Persistence.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkoutTrackerDbContext dbContext;

        private IDbContextTransaction transaction;

        public UnitOfWork(WorkoutTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            await transaction.CommitAsync();
        }
    }
}
