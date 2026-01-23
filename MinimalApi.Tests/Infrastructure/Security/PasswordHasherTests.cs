using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Infrastructure.Authentication;
using MinimalApi.Infrastructure.Exceptions;

namespace MinimalApi.Tests.Infrastructure.Security
{
    public class PasswordHasherTests
    {
        [Fact]
        public void PasswordHasher_Hash_String_Returns_HashString()
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var password = "arpandev2823@";

            //Act
            var HashedPassword = passwordHasher.Hash(password);

            //Assert
            HashedPassword.Should().NotBeNullOrEmpty();
            HashedPassword.Should().NotBeNullOrWhiteSpace();
            HashedPassword.Should().NotBe(password);
        }

        [Fact]
        public void PasswordHasher_Hash_NullString_ThrowsException()
        {
            // Arrange

            var passwordHasher = new PasswordHasher();

            // Act
            Action act = () => // short way of checking exception
            {
                var HashedPassword = passwordHasher.Hash(null);
            };

            // Assert
            act.Should().Throw<InfrastructureException>().WithMessage("Password cannot be empty");
        }

        [Theory]
        [InlineData("arpandev2823@", true)]
        [InlineData("wrongpassword", false)]
        public void PasswordHasher_Verify_HashedPassword_ReturnsBool(string passwordToTest, bool expectedResult)
        {
            // Arrange
            var passwordHasher = new PasswordHasher();
            var originalPassword = "arpandev2823@";
            var hashedPassword = passwordHasher.Hash(originalPassword);

            // Act
            var result = passwordHasher.Verify(passwordToTest, hashedPassword);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void PasswordHasher_Verify_NullOrEmptyString_Returns_False(string originalPassword)
        {
            // Arrange
            var passwordHasher = new PasswordHasher();

            var hashedPassword = passwordHasher.Hash("arpandev2823@");

            // Act
            var result = passwordHasher.Verify(originalPassword, hashedPassword);

            // Assert
            result.Should().BeFalse();
        }
    }
}