using LLMProxyAPI.DTOs;

namespace LLMProxyAPI.Services
{
    public class HuggingFaceClient
    {
        private readonly HttpClient _httpClient;
        public HuggingFaceClient(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }


        public async Task<String> GenerateResponseAsync(string prompt)
        {
            var requestBody = new
            {
                inputs = prompt
            };
            var response = await _httpClient.PostAsJsonAsync("", requestBody);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<HuggingFaceResponse>>();
            return result?.FirstOrDefault()?.GeneratedText ?? string.Empty;
        }
    }
}
