using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.Leaves;

public class CreateLeaveDto
{
    
    public Guid IdUser { get; set; }
    
    public int RemainingLeave { get; set; }

    
    public int TotalLeave { get; set; }

    
    public DateTime LeaveExpiry { get; set; }
}
