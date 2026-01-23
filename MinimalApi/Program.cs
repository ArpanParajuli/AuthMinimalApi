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
int request = 0;

app.MapGet("/", () =>
{
    request++;
    return Results.Ok(request);
});

await app.RunAsync();