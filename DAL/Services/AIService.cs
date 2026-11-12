using DAL.Api;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DAL.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AIService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> SendAsync(string prompt)
        {
            var apiKey = _configuration["AI:ApiKey"];

            var body = new
            {
                model = "gpt-4o-mini",
                messages = new object[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            var json = JsonSerializer.Serialize(body);

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://safeai613.com/v1/chat/completions"
            );

            request.Headers.Add(
                "Authorization",
                $"Bearer {apiKey}"
            );

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response =
                await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content =
                await response.Content.ReadAsStringAsync();

            using var doc =
                JsonDocument.Parse(content);

            return doc
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }
    }
}