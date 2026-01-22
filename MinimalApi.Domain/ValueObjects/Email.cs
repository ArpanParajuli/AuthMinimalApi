using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using MinimalApi.Domain.Exceptions;

namespace MinimalApi.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Email cannot be null or contains White Spaces");
            }

            return new Email(value);
        }
    }
}