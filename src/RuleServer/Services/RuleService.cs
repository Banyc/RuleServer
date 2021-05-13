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
        private readonly ILogger<RuleService> _logger;
        private readonly RuleEngine.RuleEngine _ruleEngine;
        private RuleEngineSettings ruleEngineSettings;
        public RuleEngineSettings RuleEngineSettings
        {
            get => ruleEngineSettings; set
            {
                this.ruleEngineSettings = value;
                _ruleEngine.UpdateSettings(value);
            }
        }

        public RuleService(
            IOptionsMonitor<RuleEngineSettings> settingsMonitor,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RuleEngine.RuleEngine> engineLogger,
            ILogger<RuleService> logger
            )
        {
            _settingsMonitor = settingsMonitor;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            _ruleEngine = new(_settingsMonitor.CurrentValue, engineLogger);
            this.ruleEngineSettings = _settingsMonitor.CurrentValue;

            _settingsMonitor.OnChange((settings) => this.RuleEngineSettings = settings);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
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
