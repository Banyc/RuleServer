using System.ComponentModel.DataAnnotations;

namespace RuleServer.Entities
{
    public class RuleAlertModel
    {
        [Key]
        public int WarningId { get; set; }
        public string ServerName { get; set; }
        public string SensorId { get; set; }
        public string Timestamp { get; set; }
        public string RuleDetail { get; set; }
        public string RuleName { get; set; }
        public bool IsActive { get; set; }
    }
}
