namespace WorkoutTracker.Controllers.Dto
{
    public class AuthenticationResponseDto
    {
        public AuthenticationResponseDto(int userId)
        {
            Success = true;
            UserId = userId;
        }

        public bool Success { get; set; }

        public int UserId { get; set; }
    }
}
