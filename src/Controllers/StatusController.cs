using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Controllers.Dto;
using WorkoutTracker.Persistence;

namespace WorkoutTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<StatusController> logger;
        private readonly WorkoutTrackerDbContext dbContext;

        public StatusController(
            IMapper mapper,
            ILogger<StatusController> logger,
            WorkoutTrackerDbContext dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetStatus()
        {
            logger.LogDebug($"{DateTime.Now.ToString()} | get status");

            var stateFromDb = dbContext.Status.First();
            var currentState = mapper.Map<StatusDto>(stateFromDb);

            return new OkObjectResult(currentState);
        }
    }
}
