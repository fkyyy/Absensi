using System;

namespace Application.Common.Dtos.LeaveTransactions;

public class EditLeaveTransactionDto
{
        public Guid IdTransaction { get; set; }

        public LeaveStatus Status { get; set; }

}
public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
