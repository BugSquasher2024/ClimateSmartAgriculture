using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ClimateSmartAgriculture.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Crop> Crops { get; set; }
        public JObject WeatherData { get; set; }
    }
}
