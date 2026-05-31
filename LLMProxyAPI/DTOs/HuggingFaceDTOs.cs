using System.Text.Json.Serialization;

namespace LLMProxyAPI.DTOs
{
    public record HuggingFaceResponse(
        [property: JsonPropertyName("generated_text")] string GeneratedText,
        [property: JsonPropertyName("faithfulness_score")] double FaithfulnessScore
    );

    public record HuggingFaceRequest(
        string Prompt
    );
}