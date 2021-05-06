﻿using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuleServer.Services;
using RuleServer.Models.RuleService;

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
        public IActionResult PostDatabase(IDictionary<string, object> reportModel)
        {
            _logger.LogDebug("Incoming PostDatabase.");
            _ruleService.Match("default", reportModel, this.databaseLogService.LogAlert);
            return Ok();
        }

        [HttpPost("Post")]
        public IActionResult Post(IDictionary<string, object> reportModel)
        {
            _logger.LogDebug("Incoming POST.");
            List<RuleSettings> matchedRules = new();
            _ruleService.Match("default", reportModel,
                (RuleService sender, MatchedActionArgs args) =>
                {
                    matchedRules.Add(args.Rule);
                });
            return Ok(new { Matched = matchedRules });
        }
    }
}
