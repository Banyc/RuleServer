using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuleServer.Services;

namespace RuleServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorReportController : ControllerBase
    {
        private readonly ILogger<SensorReportController> _logger;
        private readonly RuleService<string> _ruleService;
        private readonly DatabaseLogService<string> databaseLogService;

        public SensorReportController(
            ILogger<SensorReportController> logger,
            RuleService<string> ruleService,
            DatabaseLogService<string> databaseLogService)
        {
            _logger = logger;
            _ruleService = ruleService;
            this.databaseLogService = databaseLogService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost("Post")]
        public IActionResult Post(IDictionary<string, object> reportModel)
        {
            _logger.LogDebug("Incoming POST.");
            _ruleService.MatchAsync(reportModel, this.databaseLogService.LogAlert);
            return Ok();
        }
    }
}
