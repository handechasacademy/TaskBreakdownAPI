using System.Text.Json.Serialization;

namespace LLMProxyAPI.DTOs
{
    public record HuggingFaceResponse(
        [property: JsonPropertyName("generated_text")] string GeneratedText
        );

    public record HuggingFaceRequest(
        string Prompt
        );
}
