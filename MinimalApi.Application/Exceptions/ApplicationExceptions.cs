using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Exceptions
{
    public class ApplicationExceptions : Exception
    {
        public ApplicationExceptions(string message) : base(message)
        {
        }
    }
}