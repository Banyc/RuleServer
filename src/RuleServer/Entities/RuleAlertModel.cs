using System;
using System.ComponentModel.DataAnnotations;

namespace RuleServer.Entities
{
    public class RuleAlertModel
    {
        [Key]
        public int Id { get; set; }
        public string ServerName { get; set; }
        public DateTime DateTime { get; set; }
        public string RuleDetail { get; set; }
        public string GroupName { get; set; }
        public string RuleName { get; set; }
        public string Fact { get; set; }
    }
}
