namespace ContentAPI.Services
{
    public class LlmProxyClient
    {
        private readonly HttpClient _httpClient;

        public LlmProxyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateAsync(string prompt)
        {
            var requestBody = new
            {
                prompt = prompt
            };
            var response = await _httpClient.PostAsJsonAsync("/api/llm", requestBody);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
