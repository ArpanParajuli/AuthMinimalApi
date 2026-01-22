using System.Net.NetworkInformation;

namespace MinimalApi.Endpoints
{
    public record RegisterRequest(string Name, string Email, string Password);

    public record LoginRequest(string Email, string Password);

    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/register", (RegisterRequest request) =>
            {
                return Results.Ok(request);
            });

            app.MapPost("/login", (LoginRequest request) =>
            {
                return Results.Ok("Login user!");
            });

            app.MapPost("/logout", () =>
            {
            });

            app.MapPost("/refresh", () =>
            {
            });
        }
    }
}