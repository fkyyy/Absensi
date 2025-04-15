using System.Transactions;
using Application.Common.Dtos.LeaveTransactions;
using Application.LeaveTransactions.Commands;
using Application.LeaveTransactions.Queries;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class LeaveTransactionController : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<LeaveTransactionDto>>> GetTransactions()
    {
        var trs = await mediator.Send(new GetLeaveTransactionList.Query());
        if (trs == null || trs.Count == 0)
            return NotFound("No transaction found.");
        return Ok(trs);
    }

    [HttpGet("{IdTransaction}")]
    public async Task<ActionResult<LeaveTransactionDto>> GetTransactionDetail(Guid IdTransaction)
    {
        var trss = await mediator.Send(new GetLeaveTransactionById.Query { IdTransaction = IdTransaction });
        if (trss == null)
            return NotFound($"Transaction with ID {IdTransaction} not found.");
        return Ok(trss);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTransactions(CreateLeaveTransactionDto transaksi)
    {
        if (transaksi == null)
            return BadRequest("Transaction data is invalid.");
        var result = await mediator.Send(new CreateLeaveTransaction.Command { Transaction = transaksi });
        if (result == null)
            return BadRequest("Failed to create transaction.");

        return CreatedAtAction(nameof(GetTransactionDetail), new { IdTransaction = result }, result);
    }   
    [Authorize(Roles = "Leader,Admin")]
    [HttpPut("approve/{id}")]
    public async Task<IActionResult> Approve(Guid id, [FromBody] LeaveStatusDto status)
    {
        try
        {
            await mediator.Send(new ApproveLeaveTransaction.Command
            {
                IdTransaction = id,
                Status = (Application.Common.Dtos.LeaveTransactions.LeaveStatus)(int)status
            });

            return Ok(new { message = "Status leave transaction updated." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
