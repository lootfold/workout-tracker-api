using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Controllers.Dto
{
    public class SignUpDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}