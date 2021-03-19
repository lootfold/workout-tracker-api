using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Controllers.Dto;
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
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

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
        public async Task<IActionResult> SignUP([FromBody] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var user = mapper.Map<User>(signUpDto);
            var loginCreds = mapper.Map<LoginCredentials>(signUpDto);

            var newUser = await authenticationProcessor.SignUpAsync(user, loginCreds);

            return new CreatedResult($"{newUser.Id}", newUser);
        }

        [HttpPost("validate/username")]
        public async Task<IActionResult> ValidateUsername([FromBody] ValidateUsernameDto validateUsernameDto)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var isValidUsername = await authenticationProcessor.ValidateUsernameAsync(validateUsernameDto.Username);

            return new OkObjectResult(new { valid = isValidUsername });
        }
    }
}
