using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Controllers.Dto;
using WorkoutTracker.Controllers.Filters;
using WorkoutTracker.Persistence;
using WorkoutTracker.Persistence.Models;

namespace WorkoutTracker.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAuthenticationProcessor authenticationProcessor;

        public AuthenticationController(
            IMapper mapper,
            IAuthenticationProcessor authenticationProcessor)
        {
            this.mapper = mapper;
            this.authenticationProcessor = authenticationProcessor;
        }

        [HttpPost("authenticate")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var loginCreds = mapper.Map<LoginCredentials>(loginDto);
            var userId = await authenticationProcessor.ValidateCredentialsAsync(loginCreds);

            if (userId == 0)
            {
                return new UnauthorizedResult();
            }

            var response = new AuthenticationResponseDto(userId);

            return new OkObjectResult(response);
        }

        [HttpPost("signup")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> SignUP([FromBody] SignUpDto signUpDto)
        {
            var user = mapper.Map<User>(signUpDto);
            var loginCreds = mapper.Map<LoginCredentials>(signUpDto);

            var newUser = await authenticationProcessor.SignUpAsync(user, loginCreds);

            return new CreatedResult($"{newUser.Id}", newUser);
        }

        [HttpPost("validate/username")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> ValidateUsername([FromBody] ValidateUsernameDto validateUsernameDto)
        {
            var isValidUsername = await authenticationProcessor.ValidateUsernameAsync(validateUsernameDto.Username);

            return new OkObjectResult(new { valid = isValidUsername });
        }
    }
}
