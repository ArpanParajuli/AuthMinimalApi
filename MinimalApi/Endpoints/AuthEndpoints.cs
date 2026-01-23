using Microsoft.AspNetCore.Mvc;

using MinimalApi.Application.Auth;
using MinimalApi.Application.Dtos;

namespace MinimalApi.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").RequireRateLimiting("auth-policy"); ;

        // REGISTER
        group.MapPost("/register", async (
            [FromBody] RegisterRequestDto request,
            IAuthService authService,
            CancellationToken ct) =>
        {
            var result = await authService.RegisterAsync(request, ct);
            return Results.Ok(result);
        });

        // LOGIN
        group.MapPost("/login", async (
            [FromBody] LoginRequestDto request,
            IAuthService authService,
            CancellationToken ct) =>
        {
            // AuthService throws UnauthorizedAccessException if credentials fail
            var result = await authService.LoginAsync(request, ct);
            return Results.Ok(result);
        });

        // LOGOUT (Client-side usually deletes the JWT, but you can blacklist here)
        group.MapPost("/logout", () => Results.NoContent());
    }
}