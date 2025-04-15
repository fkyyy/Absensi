using Application.Common.Dtos.LeaveTransactions;
using Application.Users.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.LeaveTransactions.Commands;
public class ApproveLeaveTransaction
{
    public class Command : IRequest
    {
        public Guid IdTransaction { get; set; }
        public LeaveStatus Status { get; set; }
    }

   public class Handler(AppDbContext context, UserClaimsHelper claims) : IRequestHandler<Command>
    {
       public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(Domain.LeaveStatus), (int)request.Status))
            {
                throw new ArgumentException("Status cuti tidak valid.");
            }

            var role = claims.GetUserRole();
            var userDivision = claims.GetUserDivision();

            var transaction = await context.LeaveTransactions
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.IdTransaction == request.IdTransaction, cancellationToken);

            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaksi cuti tidak ditemukan.");
            }

            if (transaction.User == null)
            {
                throw new InvalidOperationException("Pengguna untuk transaksi tidak ditemukan.");
            }

            if (role == "Staff")
            {
                throw new UnauthorizedAccessException("Staff tidak diizinkan melakukan approve.");
            }

            if (role == "Leader" && transaction.User.IdDivision != userDivision)
            {
                throw new UnauthorizedAccessException("Leader hanya bisa approve transaksi dari divisinya.");
            }

            var leave = await context.Leaves
                .FirstOrDefaultAsync(l => l.IdUser == transaction.IdUser, cancellationToken);

            if (leave == null)
            {
                throw new KeyNotFoundException("Data cuti untuk pengguna tidak ditemukan.");
            }

            var previousStatus = transaction.Status;
            var newStatus = (Domain.LeaveStatus)(int)request.Status;

            if (previousStatus == newStatus)
            {
                return;
            }

            var totalDays = (transaction.EndDate - transaction.StartDate).Days + 1;
            if (totalDays <= 0)
            {
                throw new InvalidOperationException("Tanggal akhir harus lebih besar atau sama dengan tanggal mulai.");
            }

            using var dbTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                if (newStatus == Domain.LeaveStatus.Approved && previousStatus != Domain.LeaveStatus.Approved)
                {
                    if (leave.RemainingLeave < totalDays)
                    {
                        throw new InvalidOperationException("Sisa cuti tidak mencukupi.");
                    }
                    leave.RemainingLeave -= totalDays;
                }
                else if (newStatus == Domain.LeaveStatus.Rejected && previousStatus == Domain.LeaveStatus.Approved)
                {
                    leave.RemainingLeave += totalDays;
                }

                transaction.Status = newStatus;
                await context.SaveChangesAsync(cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

}
