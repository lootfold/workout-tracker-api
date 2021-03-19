using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Controllers.Dto
{
    public class ValidateUsernameDto
    {
        [Required]
        public string Username { get; set; }
    }
}
