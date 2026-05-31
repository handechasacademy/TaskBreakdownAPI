using ContentAPI.Data;
using ContentAPI.Filters;
using ContentAPI.Middleware;
using ContentAPI.Repositories;
using ContentAPI.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddOpenApi();

var LlmProxyAPIurl = builder.Configuration["LLMProxyAPI:BaseUrl"];
var ApiKey = builder.Configuration["ServiceAuth:ApiKey"];

// Using typed client with IHttpClientFactory to avoid socket exhaustion
builder.Services.AddHttpClient<LlmProxyClient>(client =>
{
    client.BaseAddress = new Uri(LlmProxyAPIurl!);
    client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TasksDB"));
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
