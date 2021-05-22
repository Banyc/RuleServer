using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuleServer.Services;
using RuleEngine.Models.RuleEngine;

namespace RuleServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorReportController : ControllerBase
    {
        private readonly ILogger<SensorReportController> _logger;
        private readonly RuleService _ruleService;
        private readonly DatabaseLogService databaseLogService;

        public SensorReportController(
            ILogger<SensorReportController> logger,
            RuleService ruleService,
            DatabaseLogService databaseLogService)
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

        [HttpPost("PostDatabase")]
        public IActionResult PostDatabase(IDictionary<string, object> reportModel, string targetGroupName = "default")
        {
            _logger.LogDebug("Incoming PostDatabase.");
            try
            {
                _ruleService.Match(targetGroupName, reportModel, this.databaseLogService.LogAlert, null);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
        struct ExceptionStructure
        {
            public RuleSettings Rule { get; set; }
            public string Exception { get; set; }
        }

        [HttpPost("Post")]
        public IActionResult Post(IDictionary<string, object> reportModel, string targetGroupName = "default")
        {
            _logger.LogDebug("Incoming POST.");
            List<RuleSettings> matchedRules = new();
            List<ExceptionStructure> exceptions = new();
            try
            {
                _ruleService.Match(targetGroupName, reportModel,
                    (RuleEngine.RuleEngine sender, MatchedActionArgs args) =>
                    {
                        matchedRules.Add(args.Rule);
                    },
                    (RuleEngine.RuleEngine sender, ExceptionActionArgs args) =>
                    {
                        exceptions.Add(new ExceptionStructure { Exception = args.Exception.Message, Rule = args.Rule });
                    });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(new { Matched = matchedRules, Exceptions = exceptions });
        }
    }
}
