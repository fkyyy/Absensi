using System;

namespace Application.Common.Dtos.Users;

public class EditUserDto
{
    public Guid IdUser { get; set; }
    public string Name { get; set; } = string.Empty;
}
