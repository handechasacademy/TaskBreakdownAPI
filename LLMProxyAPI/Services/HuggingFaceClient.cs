using LLMProxyAPI.DTOs;
using LLMProxyAPI.Exceptions;

namespace LLMProxyAPI.Services
{
    public class HuggingFaceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HuggingFaceClient> _logger;

        public HuggingFaceClient(HttpClient httpClient, ILogger<HuggingFaceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GenerateResponseAsync(string prompt)
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

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsJsonAsync("", requestBody);
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("HuggingFace request timed out.");
                throw new AiServiceException("AI service timed out. Please try again.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HuggingFace request failed.");
                throw new AiServiceException("Could not reach AI service.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new AiServiceException("AI service authentication failed.", 401);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                throw new AiServiceException("AI service rate limit reached.", 429);

            if (!response.IsSuccessStatusCode)
                throw new AiServiceException($"AI service returned an error.", 502);

            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();

            if (result?.Choices?[0]?.Message?.Content is null or "")
            {
                _logger.LogWarning("HuggingFace returned empty response.");
                throw new AiServiceException("AI service returned an empty response.");
            }

            return result.Choices[0].Message.Content;
        }
    }
}