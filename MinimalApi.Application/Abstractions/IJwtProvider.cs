using System;
using System.Collections.Generic;
using System.Text;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}