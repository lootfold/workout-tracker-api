namespace WorkoutTracker.Controllers.Dto
{
    public class AuthenticationResponseDto
    {
        public AuthenticationResponseDto(bool success, int userId)
        {
            Success = success;
            UserId = userId;
        }

        public bool Success { get; set; }

        public int UserId { get; set; }
    }
}
