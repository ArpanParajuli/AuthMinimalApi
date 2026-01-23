using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Application;
using MinimalApi.Endpoints;
using MinimalApi.Infrastructure;
using MinimalApi.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Management API V1");
    });
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();
int request = 0;

app.MapGet("/", () =>
{
    request++;
    return Results.Ok(request);
});

await app.RunAsync();