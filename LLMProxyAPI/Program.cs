using LLMProxyAPI.Middleware;
using LLMProxyAPI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var huggingFaceUrl = builder.Configuration["HuggingFace:ModelUrl"];
var huggingFaceKey = builder.Configuration["HuggingFace:ApiKey"];

builder.Services.AddHttpClient<HuggingFaceClient>(client =>
{
    client.BaseAddress = new Uri(huggingFaceUrl!);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {huggingFaceKey}");
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
