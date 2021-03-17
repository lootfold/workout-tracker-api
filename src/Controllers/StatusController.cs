using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Persistence;

namespace WorkoutTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> logger;
        private readonly WorkoutTrackerDbContext dbContext;

        public StatusController(ILogger<StatusController> logger, WorkoutTrackerDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            logger.LogDebug($"{DateTime.Now.ToString()} | get status");
            var currentState = dbContext.Status.First();
            return new OkObjectResult(currentState);
        }
    }
}
