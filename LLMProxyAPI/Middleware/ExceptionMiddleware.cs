using LLMProxyAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LLMProxyAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                var (statusCode, title) = ex switch
                {
                    AiServiceException aiEx => (aiEx.StatusCode, "AI Service Error"),
                    _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
                };

                var problem = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = ex.Message
                };

                httpContext.Response.ContentType = "application/problem+json";
                httpContext.Response.StatusCode = statusCode;
                var json = System.Text.Json.JsonSerializer.Serialize(problem);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}