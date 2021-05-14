using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
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
        public async Task<IActionResult> UpdateSettings(RuleEngineSettings settings)
        {
            this.ruleService.RuleEngineSettings = settings;
            const string persistencePath = "appsettings.local.ruleEngine.json";
            System.IO.File.Delete(persistencePath);
            using FileStream jsonFileStream = System.IO.File.OpenWrite(persistencePath);
            await System.Text.Json.JsonSerializer.SerializeAsync(
                jsonFileStream,
                // new { RuleService = settings },
                value: settings,
                options: new()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet("GetSettings")]
        public IActionResult GetSettings()
        {
            return Ok(this.ruleService.RuleEngineSettings);
        }
    }
}
