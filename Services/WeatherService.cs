using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ClimateSmartAgriculture.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Weather:ApiKey"];
        }

        //public async Task<JObject> GetWeatherDataAsync(string location)
        //{
        //    // Optional: Add your existing implementation for location-based weather data
        //    return null;
        //}

        public async Task<JObject> GetWeatherDataAsync(double latitude, double longitude)
        {
            var url = $"https://climate-api.open-meteo.com/v1/climate?latitude={latitude}&longitude={longitude}&start_date=2024-06-30&end_date=2024-12-31&models=MPI_ESM1_2_XR&daily=temperature_2m_mean,temperature_2m_max,temperature_2m_min,wind_speed_10m_mean,wind_speed_10m_max,shortwave_radiation_sum,relative_humidity_2m_mean,relative_humidity_2m_max,relative_humidity_2m_min,dew_point_2m_mean,dew_point_2m_min,dew_point_2m_max,precipitation_sum,rain_sum,snowfall_sum,soil_moisture_0_to_10cm_mean,et0_fao_evapotranspiration_sum";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API Error: {response.ReasonPhrase}");
                // You can throw an exception or return a specific error JObject if needed
                return new JObject { ["error"] = $"API Error: {response.ReasonPhrase}" };
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }
    }
}
