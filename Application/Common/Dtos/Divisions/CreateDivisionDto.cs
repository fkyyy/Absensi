using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.Divisions;

public class CreateDivisionDto
{
    
    public string DivisionName { get; set; } = string.Empty;
}
