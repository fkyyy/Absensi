using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Application.Common.Dtos.Attendances;

public class CreateAttendanceDto
{
    [Required]
    public Guid IdUser { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTimeOffset CheckIn { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    [Required]
    public string IsApproved { get; set; } = string.Empty;

    [Required]
    public EnumType AttendanceType { get; set; } = EnumType.Hadir; 


}
