using System;

namespace Application.Common.Dtos.Leaves;

public class LeaveDto
{
        public Guid IdLeaves { get; set; }
        public Guid IdUser { get; set; }
        public int TotalLeave { get; set; }
        public int RemainingLeave { get; set; }
        public DateTime LeaveExpiry { get; set; }
}
