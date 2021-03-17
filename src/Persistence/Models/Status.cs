using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Persistence.Models
{
    public class Status
    {
        public Status(int id, string state)
        {
            Id = id;
            State = state;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string State { get; set; }
    }
}
