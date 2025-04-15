using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Domain;
using Application.Leaves.Queries;
using Application.Common.Dtos.Leaves;
using Application.Leaves.Commands;

namespace API.Controllers;

public class LeaveController : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<LeaveDto>>> GetLeaves()
    {
        var leaves = await mediator.Send(new GetLeaveList.Query());
        if (leaves == null || leaves.Count == 0)
            return NotFound("No leaves found.");
        return Ok(leaves);
    }

    [HttpGet("{IdLeaves}")]
    public async Task<ActionResult<LeaveDto>> GetLeaveDetail(Guid IdLeaves)
    {
        var leave = await mediator.Send(new GetLeaveById.Query { IdLeaves = IdLeaves });
        if (leave == null)
            return NotFound($"Leaves with ID {IdLeaves} not found.");
        return Ok(leave);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateLeaves(CreateLeaveDto leave)
    {
        if (leave == null)
            return BadRequest("Leave data is invalid.");
        try
        {
            var result = await mediator.Send(new CreateLeave.Command { Leave = leave });

            if (string.IsNullOrEmpty(result))
                return BadRequest("Failed to create Leave.");

            return CreatedAtAction(
                nameof(GetLeaveDetail),
                new { IdLeaves = result },
                result
            );
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPut("{IdLeaves}")]
    public async Task<ActionResult> EditLeave(EditLeaveDto leave)
    { 
        if (leave == null)
            return BadRequest("leave data is invalid.");
        await mediator.Send(new EditLeave.Command { Leave = leave });
        return NoContent();
    }
}