using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Controllers.Dto;
using WorkoutTracker.Persistence;
using WorkoutTracker.Persistence.Interfaces;
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
        private readonly ILoginRepository loginRepository;

        public AuthenticationController(
            IMapper mapper,
            ILogger<AuthenticationController> logger,
            WorkoutTrackerDbContext dbContext,
            ILoginRepository loginRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.dbContext = dbContext;
            this.loginRepository = loginRepository;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var loginCreds = mapper.Map<LoginCredentials>(loginDto);
            var userId = await loginRepository.GetUserIdByCredentialsAsync(loginCreds);

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