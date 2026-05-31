using LLMProxyAPI.Middleware;
using LLMProxyAPI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var huggingFaceUrl = builder.Configuration["HuggingFace:ModelUrl"];
var huggingFaceKey = builder.Configuration["HuggingFace:ApiKey"];

// Using typed client with IHttpClientFactory to avoid socket exhaustion
builder.Services.AddHttpClient<HuggingFaceClient>(client =>
{
    client.BaseAddress = new Uri(huggingFaceUrl!);
    client.Timeout = TimeSpan.FromSeconds(30); // ← lägg till denna
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {huggingFaceKey}");
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>(); 
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
