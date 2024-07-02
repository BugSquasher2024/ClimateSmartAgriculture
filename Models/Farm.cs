using System.ComponentModel.DataAnnotations;

namespace ClimateSmartAgriculture.Models
{
    public class Farm
    {
        [Key]
        public int FarmId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Size must be greater than zero")]
        public double Size { get; set; }

        [Required]
        [StringLength(50)]
        public string ClimateZone { get; set; }

        public ICollection<SoilMoisture> SoilMoistureReadings { get; set; }  // Navigation property
    }
}