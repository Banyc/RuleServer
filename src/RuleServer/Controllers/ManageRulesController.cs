using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuleEngine.Models.RuleEngine;
using RuleServer.Services;

namespace src.RuleServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManageRulesController : ControllerBase
    {
        private readonly ILogger<ManageRulesController> logger;
        private readonly RuleService ruleService;

        public ManageRulesController(
            ILogger<ManageRulesController> logger,
            RuleService ruleService
        )
        {
            this.logger = logger;
            this.ruleService = ruleService;
        }

        [HttpPut("UpdateSettings")]
        public IActionResult UpdateSettings(RuleEngineSettings settings)
        {
            this.ruleService.RuleEngineSettings = settings;
            return Ok();
        }

        [HttpGet("GetSettings")]
        public IActionResult GetSettings()
        {
            return Ok(this.ruleService.RuleEngineSettings);
        }
    }
}
