using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.RateLimiting;
using MinimalApi.Application;
using MinimalApi.Endpoints;
using MinimalApi.Infrastructure;
using MinimalApi.Infrastructure.Authentication;

using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("auth-policy", httpContext =>
    {
        // 1. Handle Load Balancers/Proxies to get the real Client IP
        var clientIp = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                     ?? httpContext.Connection.RemoteIpAddress?.ToString()
                     ?? "unknown";

        // 2. IP Whitelisting (Optional: skip limiting for admin IPs)

        //if (clientIp == "127.0.0.1" || clientIp == "::1")
        //{
        //    return RateLimitPartition.GetNoLimiter(clientIp);
        //}

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: clientIp,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromSeconds(60),
                QueueLimit = 0,
                AutoReplenishment = true
            });
    });

    // 3. Structured Problem Details Response
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/problem+json";

        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Status = StatusCodes.Status429TooManyRequests,
            Title = "Too Many Requests",
            Detail = "Rate limit exceeded. Try again in 60 seconds.",
            Instance = context.HttpContext.Request.Path
        }, cancellationToken: token);
    };
});

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
app.UseRateLimiter();

app.UseHttpsRedirection();

app.MapAuthEndpoints();
int request = 0;

app.MapGet("/", () =>
{
    request++;
    return Results.Ok(request);
}).RequireRateLimiting("auth-policy");

await app.RunAsync();