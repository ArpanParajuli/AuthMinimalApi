using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Exceptions;
using MinimalApi.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace MinimalApi.Tests.Domain.Entities
{
    public class UserTests
    {
        [Fact]
        public void User_Create_ValidEmailAndPassword_CreatesValidObject()
        {
            //Arrange
            string DummyEmail = "arpanparauli@gmail.com";
            string DummyPassword = "arpandev282";
            Password password = Password.Create(DummyPassword);
            Email email = Email.Create(DummyEmail);

            // Act
            User user = new User(password, email);

            // Assert
            user.Email.Should().Be(email);
            user.Password.Should().Be(password);
            user.IsActive.Should().BeTrue();
        }

        [Fact]
        public void User_Create_NullEmail_ThrowsException()
        {
            // Arrange
            string dummyPassword = "ydywa27287278272";
            Password password = Password.Create(dummyPassword);

            // Act

            var expection = Assert.Throws<DomainException>(() =>
            {
                new User(password, null); // email null
            });

            // Assert

            Assert.Equal("Email required", expection.Message);
        }

        [Fact]
        public void User_Create_NullPassword_ThrowsException()
        {
            // Arrange
            string dummyEmail = "arpanparajuli388@gmail.com";
            Email email = Email.Create(dummyEmail);

            // Act

            var expection = Assert.Throws<DomainException>(() =>
            {
                new User(null, email); // password null
            });

            // Assert

            Assert.Equal("Password required", expection.Message);
        }
    }
}