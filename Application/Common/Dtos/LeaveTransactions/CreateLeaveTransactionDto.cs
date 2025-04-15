using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos.LeaveTransactions;

public class CreateLeaveTransactionDto
{

    [Required]
    public DateTime RequestDate { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public LeaveStatusType Status { get; set; }
}

public enum LeaveStatusType
{
    Pending,
    Approved,
    Rejected
}
