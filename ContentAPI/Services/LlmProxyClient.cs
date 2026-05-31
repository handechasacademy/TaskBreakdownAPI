using System.Text.Json.Serialization;

namespace ContentAPI.Services
{
    public record LlmProxyResponse(
        [property: JsonPropertyName("generated_text")] string GeneratedText,
        [property: JsonPropertyName("faithfulness_score")] double FaithfulnessScore
    );

    public class LlmProxyClient
    {
        private readonly HttpClient _httpClient;

        public LlmProxyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateAsync(string prompt)
        {
            var requestBody = new { prompt = prompt };
            var response = await _httpClient.PostAsJsonAsync("/api/llm", requestBody);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LlmProxyResponse>();
            return result?.GeneratedText ?? string.Empty;
        }
    }
}