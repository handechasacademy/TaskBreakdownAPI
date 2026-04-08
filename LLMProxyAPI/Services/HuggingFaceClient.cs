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
                inputs = fullPrompt
            };
            var response = await _httpClient.PostAsJsonAsync("", requestBody);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<HuggingFaceResponse>>();
            return result?.FirstOrDefault()?.GeneratedText ?? string.Empty;
        }
    }
}
