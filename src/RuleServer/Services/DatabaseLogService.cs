using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RuleServer.Data;
using RuleEngine.Models.RuleEngine;

namespace RuleServer.Services
{
    public class DatabaseLogService
    {
        private readonly ILogger<DatabaseLogService> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DatabaseLogService(ILogger<DatabaseLogService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async Task LogAlertAsync(RuleEngine.RuleEngine sender, MatchedActionArgs args)
        {
            this.logger.LogDebug("Start writing database.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using var scope = this.serviceScopeFactory.CreateScope();
            var logDatabase = scope.ServiceProvider.GetService<RuleAlertContext>();
            await logDatabase.RuleAlerts.AddAsync(new()
            {
                RuleDetail = args.Rule.ConditionExpression,
                RuleName = args.Rule.RuleName,
                DateTime = DateTime.Now,
                Fact = System.Text.Json.JsonSerializer.Serialize(args.Arguments),
                ServerName = sender.ServerName,
                GroupName = args.Group.GroupName,
            }).ConfigureAwait(false);
            await logDatabase.SaveChangesAsync().ConfigureAwait(false);
            stopwatch.Stop();
            this.logger.LogDebug($"Done writing database. Time span: {stopwatch.Elapsed.TotalSeconds:0.####}s");
        }
        // non-blocking
        public void LogAlert(RuleEngine.RuleEngine sender, MatchedActionArgs args)
        {
            Task.Run(async () => await LogAlertAsync(sender, args).ConfigureAwait(false));
        }
        // blocking
        public async void LogAlertBlocking(RuleEngine.RuleEngine sender, MatchedActionArgs args)
        {
            await LogAlertAsync(sender, args).ConfigureAwait(false);
        }
    }
}
