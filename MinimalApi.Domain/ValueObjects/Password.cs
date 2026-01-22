using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Domain.ValueObjects
{
    internal class Password
    {
        public string password { get; }

        private Password(string password)
        {
            this.password = password;
        }

        public Password Create(string value)
        {
            return new Password(value);
        }
    }
}