using System.IO;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuleEngine.Models.RuleEngine;

namespace RuleServer.Services
{
    // singleton
    public partial class RuleService : IHostedService
    {
        public string ServerName { get => _ruleEngine.ServerName; }
        private readonly IOptionsMonitor<RuleEngineSettings> _settingsMonitor;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<RuleEngine.RuleEngine> _engineLogger;
        private readonly ILogger<RuleService> _logger;
        private RuleEngine.RuleEngine _ruleEngine;
        private RuleEngineSettings ruleEngineSettings;
        public RuleEngineSettings RuleEngineSettings
        {
            get => ruleEngineSettings; set
            {
                // this line must run first to trigger potential exceptions
                _ruleEngine.UpdateSettings(value);
                this.ruleEngineSettings = value;
            }
        }

        private readonly string persistencePath = "appsettings.local.ruleEngine.json";

        public RuleService(
            IOptionsMonitor<RuleEngineSettings> settingsMonitor,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RuleEngine.RuleEngine> engineLogger,
            ILogger<RuleService> logger
            )
        {
            _settingsMonitor = settingsMonitor;
            _serviceScopeFactory = serviceScopeFactory;
            _engineLogger = engineLogger;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (File.Exists(this.persistencePath))
            {
                // this settings source is considered as up-to-dated one
                using var fileStream = File.OpenRead(this.persistencePath);
                this.ruleEngineSettings = await System.Text.Json.JsonSerializer.DeserializeAsync<RuleEngineSettings>(fileStream).ConfigureAwait(false);
            }
            else
            {
                // this settings source is considered as old one
                this.ruleEngineSettings = _settingsMonitor.CurrentValue;
            }
            _ruleEngine = new(_settingsMonitor.CurrentValue, _engineLogger);

            _settingsMonitor.OnChange((settings) => {
                // don't load old settings when there are new settings
                if (!File.Exists(persistencePath))
                {
                    this.RuleEngineSettings = settings;
                }
            });
            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _ruleEngine.Dispose();
            return Task.CompletedTask;
        }

        public void Match(string groupName, IDictionary<string, object> symbolTable,
            Action<RuleEngine.RuleEngine, MatchedActionArgs> matchingAction,
            Action<RuleEngine.RuleEngine, ExceptionActionArgs> exceptionAction)
        {
            _ruleEngine.Match(groupName, symbolTable, matchingAction, exceptionAction);
        }
    }
}
