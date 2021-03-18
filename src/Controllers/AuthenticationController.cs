using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthenticationController> logger;
        private readonly WorkoutTrackerDbContext dbContext;

        public AuthenticationController(
            IMapper mapper,
            ILogger<AuthenticationController> logger,
            WorkoutTrackerDbContext dbContext)
        {
            this.mapper = mapper;
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
                .SingleAsync(i => i.Username == loginDto.Username && i.Password == loginDto.Password);

            logger.LogDebug($"{DateTime.Now}| login creds valid: {loginCreds != null}");
            var response = new AuthenticationResponseDto(loginCreds != null, loginCreds.UserId);

            return new OkObjectResult(response);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUP([FromBody] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogDebug($"{DateTime.Now} | invalid request object");
                return new BadRequestObjectResult(ModelState);
            }

            var user = mapper.Map<User>(signUpDto);

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            logger.LogDebug($"{DateTime.Now} | added user to DB");

            var loginCreds = mapper.Map<LoginCredentials>(signUpDto);
            loginCreds.UserId = user.Id;

            await dbContext.LoginCredentials.AddAsync(loginCreds);
            await dbContext.SaveChangesAsync();
            logger.LogDebug($"{DateTime.Now} | added login creds to DB");

            transaction.Commit();

            return new CreatedResult($"{user.Id}", user);
        }
    }
}