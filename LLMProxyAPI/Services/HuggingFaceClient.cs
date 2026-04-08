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
            var fullPrompt = $"""
                            You are an ADHD/autism-friendly task assistant. 
                            Break down the following goal into small, clear microsteps.
                            Also provide a scare factor from 1-10 if not given.
                            Be encouraging and supportive.
    
                            Goal: {prompt}
                            """;
            var requestBody = new
            {
                model = "Qwen/Qwen2.5-7B-Instruct",
                messages = new[]
            {
                new { role = "user", content = fullPrompt }
            }
                    };
                    var response = await _httpClient.PostAsJsonAsync("", requestBody);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
                    return result?.Choices?[0]?.Message?.Content ?? string.Empty;
        }
    }
}
