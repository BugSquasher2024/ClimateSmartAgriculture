using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimateSmartAgriculture.Models
{
    public class Crop
    {
        public int CropId { get; set; }

        [Required]
        public int FarmId { get; set; }

        [ForeignKey("FarmId")]
        public Farm Farm { get; set; }

        [Required]
        public string CropType { get; set; }

        [Required]
        public DateTime PlantingDate { get; set; }

        [Required]
        public DateTime HarvestDate { get; set; }
    }
}
