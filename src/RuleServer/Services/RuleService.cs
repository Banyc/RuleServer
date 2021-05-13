using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RuleEngine.Models.RuleService;

namespace RuleServer.Services
{
    // singleton
    public partial class RuleService : IHostedService
    {
        public string ServerName { get => _ruleService.ServerName; }
        private readonly IOptionsMonitor<RuleServiceSettings> _settingsMonitor;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<RuleService> _logger;
        private readonly RuleEngine.RuleService _ruleService;

        public RuleService(
            IOptionsMonitor<RuleServiceSettings> settingsMonitor,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<RuleEngine.RuleService> engineLogger,
            ILogger<RuleService> logger
            )
        {
            _settingsMonitor = settingsMonitor;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            _ruleService = new(_settingsMonitor.CurrentValue, engineLogger);
            _settingsMonitor.OnChange(_ruleService.UpdateSettings);
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
            Action<RuleEngine.RuleService, MatchedActionArgs> matchingAction,
            Action<RuleEngine.RuleService, ExceptionActionArgs> exceptionAction)
        {
            _ruleService.Match(groupName, symbolTable, matchingAction, exceptionAction);
        }
    }
}
