using LLMProxyAPI.Services;
using LLMProxyAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LLMProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LlmController : ControllerBase
    {
        private readonly HuggingFaceClient _huggingFaceClient;

        public LlmController(HuggingFaceClient huggingFaceClient)
        {
            _huggingFaceClient = huggingFaceClient;
        }

        /// <summary>
        /// Generates a response using the provided prompt and returns the result as an HTTP response.
        /// </summary>
        /// <param name="request">The request object containing the prompt to be processed. Cannot be null.</param>
        /// <returns>An <see cref="IActionResult"/> containing the generated response and faithfulness score.</returns>
        [HttpPost]
        public async Task<IActionResult> GenerateResponseAsync([FromBody] HuggingFaceRequest request)
        {
            var response = await _huggingFaceClient.GenerateResponseAsync(request.Prompt);

            var responseWords = response.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var promptWords = request.Prompt.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var relevantWords = responseWords.Count(w =>
                promptWords.Any(p => w.Contains(p, StringComparison.OrdinalIgnoreCase)));
            var faithfulnessScore = responseWords.Length > 0
                ? Math.Round((double)relevantWords / responseWords.Length, 2)
                : 0.0;

            return Ok(new HuggingFaceResponse(response, faithfulnessScore));
        }
    }
}