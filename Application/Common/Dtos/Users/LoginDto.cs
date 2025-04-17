using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.Users;

public class LoginDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
