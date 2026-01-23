using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Dtos
{
    public record AuthResponse(
     string Token,
     UserResponse User);
}