using ContentAPI.Data;
using ContentAPI.Filters;
using ContentAPI.Repositories;
using ContentAPI.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddHttpClient<LlmProxyClient>(client =>
{
    client.BaseAddress = new Uri(LlmProxyAPIurl!);
    client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TasksDB"));
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
