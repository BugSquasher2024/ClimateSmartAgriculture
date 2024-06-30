using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClimateSmartAgriculture.Services;

namespace ClimateSmartAgriculture.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;
        private readonly GeocodingService _geocodingService;

        public WeatherController(WeatherService weatherService, GeocodingService geocodingService)
        {
            _weatherService = weatherService;
            _geocodingService = geocodingService;
        }

        public async Task<IActionResult> Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                location = "Conestoga Station, Waterloo, Canada";
            }

            var (latitude, longitude) = await _geocodingService.GetCoordinatesAsync(location);
            var weatherData = await _weatherService.GetWeatherDataAsync(latitude, longitude);
            return View(weatherData);
        }
    }
}
