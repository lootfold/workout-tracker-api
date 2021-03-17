using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Persistence.Models
{
    public class LoginCredentials
    {
        public LoginCredentials(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
