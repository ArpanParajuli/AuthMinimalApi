using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Application.Abstractions;
using MinimalApi.Infrastructure.Authentication;
using MinimalApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MinimalApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();

            return services;
        }
    }
}