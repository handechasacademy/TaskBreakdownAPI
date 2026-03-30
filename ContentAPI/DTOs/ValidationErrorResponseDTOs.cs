namespace ContentAPI.DTOs
{
    public record ValidationErrorResponse(
    string Message,
    IEnumerable<string> Errors
);
}
