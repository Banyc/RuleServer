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

        public SensorReportController(ILogger<SensorReportController> logger, RuleService<string> ruleService)
        {
            _logger = logger;
            _ruleService = ruleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost("Post")]
        public async Task<IActionResult> PostAsync(IDictionary<string, object> reportModel)
        {
            // foreach (var element in reportModel)
            // {
            //     element.Value.
            // }
            // 
            _ruleService.Alert(reportModel, _ruleService.LogAlert);

            return Ok();
        }
    }
}
