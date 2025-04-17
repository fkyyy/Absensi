using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.Leaves;

public class CreateLeaveDto
{
    [Required]
    public Guid IdUser { get; set; }
    [Required]
    public int RemainingLeave { get; set; }

    [Required]
    public int TotalLeave { get; set; }

    [Required]
    public DateTime LeaveExpiry { get; set; }
}
