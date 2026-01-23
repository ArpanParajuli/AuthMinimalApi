using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MinimalApi.Domain.Exceptions;
using MinimalApi.Domain.ValueObjects;

namespace MinimalApi.Tests.Domain.ValueObjects
{
    public class PasswordTests
    {
        [Theory]
        [InlineData("arpandev132@")]
        public void Password_InputValid_Returns_Value(string value)
        {
            // Act

            var password = Password.Create(value);

            // Assert

            password.Value.Should().Be(value);
            password.Value.Should().NotBeNullOrEmpty();
            password.Value.Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Password_Input_Null_Returns_Value(string value)
        {
            var PasswordExcepction = Assert.Throws<DomainException>(() =>
            {
                var password = Password.Create(value);
            });

            Assert.Equal("Password cannot be null or empty", PasswordExcepction.Message);
        }
    }
}