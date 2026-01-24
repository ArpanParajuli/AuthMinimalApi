using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Dtos
{
    public record RegisterRequestDto(
      string Email,
      string Password,
      string FullName);
}