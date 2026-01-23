using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Dtos
{
    public record UserResponse(
     Guid Id,
     string Email,
     DateTime CreatedAt);
}