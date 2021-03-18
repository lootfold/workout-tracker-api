using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Persistence.Models
{
    public class LoginCredentials
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
