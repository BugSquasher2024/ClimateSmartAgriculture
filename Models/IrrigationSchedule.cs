using ClimateSmartAgriculture.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimateSmartAgricultureSystem.Models
{
    public class IrrigationSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [ForeignKey("Farm")]
        public int FarmId { get; set; }
        public Farm Farm { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public double WaterAmount { get; set; }
    }
}
