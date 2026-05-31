# AI Content Assistant

A distributed microservices system built with ASP.NET Core Web API. The system helps users with ADHD/autism break down goals into manageable microsteps using an LLM.

## Projects

- **ContentAPI** (Service A) - Handles CRUD for task breakdowns, runs on `https://localhost:7070`
- **LLMProxyAPI** (Service B) - Proxies requests to HuggingFace LLM, runs on `https://localhost:7084`

## Starting Both Projects

1. Open the solution in Visual Studio
2. Right-click the Solution -> **Properties**
3. Select **Multiple Startup Projects**
4. Set both `ContentAPI` and `LLMProxyAPI` to **Start**
5. Press `F5` to run both services simultaneously
6. Scalar UI will open at `https://localhost:7070/scalar/v1`

## Setting Up API Keys Locally (User Secrets)

### Service A (ContentAPI)

```bash
cd ContentAPI
dotnet user-secrets init
dotnet user-secrets set "ServiceAuth:ApiKey" "your-shared-secret-key"
dotnet user-secrets set "LLMProxyAPI:BaseUrl" "https://localhost:7084"
```

### Service B (LLMProxyAPI)

```bash
cd LLMProxyAPI
dotnet user-secrets set "HuggingFace:ApiKey" "your-huggingface-token"
dotnet user-secrets set "ServiceAuth:ApiKey" "your-shared-secret-key"
```

> Both services must use the **same** `ServiceAuth:ApiKey` value.
> Get your HuggingFace token at: https://huggingface.co/settings/tokens

## Setting Up API Keys in Production (Environment Variables)

Never store API keys in `appsettings.json` or commit them to the repo.

In production, set environment variables instead:

```bash
export HuggingFace__ApiKey="your-huggingface-token"
export ServiceAuth__ApiKey="your-shared-secret-key"
```

ASP.NET Core automatically reads these (double underscore = nested config).

## Key Security Guarantees

- No API keys in `appsettings.json` or any committed file
- No logging of Authorization headers or API keys
- ProblemDetails error responses never expose internal keys or stack traces

## Custom Exception Middleware

The `ExceptionMiddleware` in both **ContentAPI** and **LLMProxyAPI** intercepts all unhandled exceptions before they reach the client. It:

- Logs the full exception using `ILogger`
- Maps exception types to HTTP status codes:
  - `NotFoundException` -> `404 Not Found`
  - `ValidationException` -> `400 Bad Request`
  - `AiServiceException` -> `401 / 429 / 502` (based on HuggingFace error)
  - Everything else -> `500 Internal Server Error`
- Returns a structured `ProblemDetails` response (RFC 7807) with `Content-Type: application/problem+json`

### Triggering a ProblemDetails response

Send a GET request to a non-existent task ID in Scalar:

```
GET https://localhost:7070/api/Tasks/999
```

You will receive:

```json
{
  "title": "Not Found",
  "status": 404,
  "detail": "Task with id 999 was not found."
}
```