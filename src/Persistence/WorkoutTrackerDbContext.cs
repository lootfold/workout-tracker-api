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

        public DbSet<LoginCredentials> LoginCredentials { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LoginCredentials>()
                .HasIndex(l => l.Username).IsUnique();

            builder.Entity<Status>().HasData(new Status(1, "Ok"));

            builder.Entity<User>()
                .HasData(new User()
                {
                    Id = 1,
                    Name = "Pallav Dubey"
                });

            builder.Entity<LoginCredentials>()
                .HasData(new LoginCredentials()
                {
                    Id = 1,
                    Username = "lootfold",
                    Password = "password",
                    UserId = 1
                });
        }
    }
}