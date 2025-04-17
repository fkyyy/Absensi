using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.LeaveTransactions;

public class CreateLeaveTransactionDto
{

    
    public DateTime RequestDate { get; set; }

    
    public DateTime StartDate { get; set; }

    
    public DateTime EndDate { get; set; }

    
    public LeaveStatusType Status { get; set; }
}

public enum LeaveStatusType
{
    Pending,
    Approved,
    Rejected
}
