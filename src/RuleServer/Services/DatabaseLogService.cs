using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RuleServer.Data;
using RuleServer.Models.RuleService;

namespace RuleServer.Services
{
    public class DatabaseLogService<TSensor>
    {
        private readonly ILogger<DatabaseLogService<TSensor>> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DatabaseLogService(ILogger<DatabaseLogService<TSensor>> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async void LogAlert(RuleService<TSensor> sender, MatchedActionArgs args)
        {
            this.logger.LogDebug("Start writing database.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using var scope = this.serviceScopeFactory.CreateScope();
            var logDatabase = scope.ServiceProvider.GetService<RuleAlertContext>();
            await logDatabase.RuleAlerts.AddAsync(new()
            {
                IsActive = true,
                RuleDetail = args.Rule.ConditionExpression,
                RuleName = args.Rule.RuleName,
                Timestamp = (string)args.Arguments["timestamp"],
                SensorId = (string)args.Arguments["sensorId"],
                ServerName = sender.ServerName
            }).ConfigureAwait(false);
            await logDatabase.SaveChangesAsync().ConfigureAwait(false);
            stopwatch.Stop();
            this.logger.LogDebug($"Done writing database. Time span: {stopwatch.Elapsed.TotalSeconds:0.####}s");
        }
    }
}
