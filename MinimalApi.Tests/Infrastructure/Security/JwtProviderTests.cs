using FluentAssertions;
using Microsoft.Extensions.Options;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.ValueObjects;
using MinimalApi.Infrastructure.Authentication;
using MinimalApi.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Tests.Infrastructure.Security
{
    public class JwtProviderTests
    {
        [Fact]
        public void JwtProvider_Generate_ReturnsValidToken()
        {
            var jwtOptions = new JwtOptions
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                SecretKey = "THIS_IS_A_SUPER_SECRET_TEST_KEY_123456",
                ExpiryInMinutes = 60
            };

            var options = Options.Create(jwtOptions);

            var jwtProvider = new JwtProvider(options);

            string DummyEmail = "arpanparauli@gmail.com";
            string DummyPassword = "arpandev282";
            Password password = Password.Create(DummyPassword);
            Email email = Email.Create(DummyEmail);

            // Act
            User user = new User(password, email);

            var token = jwtProvider.Generate(user);

            token.Should().NotBeNullOrWhiteSpace();
            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void JwtProvider_Generate_NullUser_ThrowsException()
        {
            var jwtOptions = new JwtOptions
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                SecretKey = "THIS_IS_A_SUPER_SECRET_TEST_KEY_123456",
                ExpiryInMinutes = 60
            };

            var options = Options.Create(jwtOptions);

            var jwtProvider = new JwtProvider(options);

            var JwtException = Assert.Throws<InfrastructureException>(() =>
            {
                var token = jwtProvider.Generate(null);
            });

            Assert.Equal("User cannot be null", JwtException.Message);
        }
    }
}