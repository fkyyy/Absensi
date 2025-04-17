using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.Users;
public class CreateUserDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    
    public string Status { get; set; } = string.Empty;

    
    public Guid IdDivision { get; set; }
}

