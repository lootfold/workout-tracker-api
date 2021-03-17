namespace WorkoutTracker.Controllers.Dto
{
    public class AuthenticationResponseDto
    {
        public AuthenticationResponseDto(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}
