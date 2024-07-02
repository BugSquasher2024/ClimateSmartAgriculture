using System;

namespace ClimateSmartAgriculture.Models
{
    public class SoilMoisture
    {
        public int MoistureId { get; set; }  // Primary key
        public int FarmId { get; set; }
        public DateTime Date { get; set; }
        public double Level { get; set; }

        public Farm Farm { get; set; }  // Navigation property
    }
}
