using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ClimateSmartAgriculture.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeocodingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenCage:ApiKey"];
        }

        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string locationAddress)
        {
            string baseUrl = "https://api.opencagedata.com/geocode/v1/json";
            string apiKey = _apiKey;
            string address = locationAddress;

            // Encode the address for URL
            string encodedAddress = Uri.EscapeDataString(address);

            // Construct the full URL with query parameters
            string url = $"{baseUrl}?q={encodedAddress}&key={apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Error: {response.ReasonPhrase}");
                    throw new Exception($"API Error: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                var results = json["results"];
                if (results != null && results.Any())
                {
                    var geometry = results[0]["geometry"];
                    double latitude = geometry["lat"].Value<double>();
                    double longitude = geometry["lng"].Value<double>();
                    return (latitude, longitude);
                }

                throw new Exception("No coordinates found for the given location.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw new Exception("An error occurred while fetching coordinates.", e);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
    }
}
