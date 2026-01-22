using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MinimalApi.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}