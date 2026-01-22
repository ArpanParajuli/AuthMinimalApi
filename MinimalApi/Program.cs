using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();

app.MapGet("/student/{name}", (string name) =>
{
    return $"hello , {name}";
}).RequireAuthorization();

await app.RunAsync();