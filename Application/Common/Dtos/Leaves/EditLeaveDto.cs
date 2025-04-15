using System;

namespace Application.Common.Dtos.Leaves;

public class EditLeaveDto
{
    public Guid IdLeaves { get; set; }
    public int RemainingLeave { get; set; }
    public int TotalLeave { get; set; }
}
