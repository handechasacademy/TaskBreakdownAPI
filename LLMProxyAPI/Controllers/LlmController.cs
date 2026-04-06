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
        /// <returns>An <see cref="IActionResult"/> containing the generated response from the Hugging Face client.</returns>
        [HttpPost]

        public async Task<IActionResult> GenerateResponseAsync ([FromBody] HuggingFaceRequest request)
        {
            var response = await _huggingFaceClient.GenerateResponseAsync(request.Prompt);
            return Ok(response);
        }

    }
}
