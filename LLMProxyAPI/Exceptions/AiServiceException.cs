namespace LLMProxyAPI.Exceptions
{
    public class AiServiceException : Exception
    {
        public int StatusCode { get; }

        public AiServiceException(string message, int statusCode = 502)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}