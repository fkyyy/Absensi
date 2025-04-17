using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Application.Common.Dtos.Attendances;

public class AbsenDto
{
    
    public Guid IdUser { get; set; }

    
    public DateTime Date { get; set; }

    
    public DateTimeOffset CheckIn { get; set; }

    
    public string Status { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    
    public string IsApproved { get; set; } = string.Empty;

    
    public EnumType AttendanceType { get; set; } = EnumType.Sakit; 

    public Guid? AttachmentId { get; set; }

}
