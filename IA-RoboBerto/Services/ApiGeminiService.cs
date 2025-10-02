using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IA_RoboBerto.Services
{
    public class ApiGeminiService
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiKey;

        public ApiGeminiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient; 
            _apiKey = config["Gemini:ApiKey"]; 
        }

        public async Task<string> PerguntarGeminiAsync(string prompt)
        {
            var request = new
            {
                contents = new[] {
                    new {
                        role = "user",
                        parts = new[] { new { text = prompt } } 
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}",
                content
            );

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}