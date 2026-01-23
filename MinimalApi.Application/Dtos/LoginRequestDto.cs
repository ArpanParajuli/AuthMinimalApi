using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Dtos
{
    public record LoginRequestDto(
    string Email,
    string Password);
}