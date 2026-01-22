using MinimalApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MinimalApi.Domain.ValueObjects
{
    public class Password
    {
        public string Value { get; }

        private Password(string password)
        {
            this.Value = password;
        }

        public static Password Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Password cannot be null or empty");
            }

            return new Password(value);
        }
    }
}