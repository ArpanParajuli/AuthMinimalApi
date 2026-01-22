using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MinimalApi.Domain.ValueObjects
{
    internal class Email
    {
        public string email { get; }

        private Email(string value)
        {
            email = value;
        }

        public static Email Create(string value)
        {
            return new Email(value);
        }
    }
}