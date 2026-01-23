using MinimalApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequestDto request, CancellationToken ct = default);

        Task<AuthResponse> LoginAsync(LoginRequestDto request, CancellationToken ct = default);
    }
}