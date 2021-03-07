using Microsoft.EntityFrameworkCore;
using RuleServer.Entities;

namespace RuleServer.Data
{
    public class RuleAlertContext : DbContext
    {
        public RuleAlertContext(DbContextOptions<RuleAlertContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        public DbSet<RuleAlertModel> RuleAlerts { get; set; }
    }
}
