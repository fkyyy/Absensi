using System;

namespace Application.Common.Dtos.Divisions;

public class DivisionDto
{
    public Guid IdDivision { get; set; }
    public string DivisionName { get; set; } = string.Empty;
}
