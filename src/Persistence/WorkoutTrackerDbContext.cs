using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Persistence
{
    public class WorkoutTrackerDbContext : DbContext
    {
        public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>().HasData(new Status(1, "Ok"));
        }
    }
}