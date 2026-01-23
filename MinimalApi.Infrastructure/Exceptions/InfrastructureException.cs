using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message)
        {
        }
    }
}