using System.Threading.Tasks;

namespace WorkoutTracker.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();

        Task SaveAsync();

        Task CommitAsync();
    }
}
