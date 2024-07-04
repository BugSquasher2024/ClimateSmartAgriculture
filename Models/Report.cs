using ClimateSmartAgriculture.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimateSmartAgricultureSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string ReportType { get; set; }
        public string ReportData { get; set; }
        public DateTime GeneratedOn { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
