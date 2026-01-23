using FluentAssertions;
using MinimalApi.Domain.Exceptions;
using MinimalApi.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Tests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("arpanparajuli@8282gmail.com")]
        public void Email_Input_Valid_Returns_Value(string value)
        {
            // Act

            var email = Email.Create(value);

            // Assert

            email.Value.Should().Be(value);
            email.Value.Should().Contain("@");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Email_Input_Null_Returns_Exception(string value)
        {
            // Act

            var exception = Assert.Throws<DomainException>(() =>
            {
                var email = Email.Create(value);
            });

            Assert.Equal("Email cannot be null or contains White Spaces", exception.Message);
        }

        [Theory]
        [InlineData("arpanparajuli8282gmail.com")]
        public void Email_Input_Invalid_Returns_Exception(string value)
        {
            // Act

            var exception = Assert.Throws<DomainException>(() =>
            {
                var email = Email.Create(value);
            });

            Assert.Equal("Email must contain '@'", exception.Message);
        }
    }
}