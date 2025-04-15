using System;

namespace Application.Common.Dtos.LeaveTransactions;
    public class LeaveTransactionDto
    {
        public Guid IdTransaction { get; set; }
        public Guid IdUser { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatusDto Status { get; set; }
        public Guid IdDivision { get; set; }
    }

    public enum LeaveStatusDto
    {
        Pending,
        Approved,
        Rejected
    }
