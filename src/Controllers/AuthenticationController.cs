using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Controllers.Dto;
using WorkoutTracker.Persistence;

namespace WorkoutTracker.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly WorkoutTrackerDbContext dbContext;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            WorkoutTrackerDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogDebug($"{DateTime.Now} | invalid request object");
                return new BadRequestObjectResult(ModelState);
            }

            var loginCreds = await dbContext.LoginCredentials
                                .SingleAsync(i => i.Username == loginDto.Username
                                                && i.Password == loginDto.Password);

            logger.LogDebug($"{DateTime.Now}| login creds valid: {loginCreds != null}");
            var response = new AuthenticationResponseDto(loginCreds != null);

            return new OkObjectResult(response);
        }
    }
}