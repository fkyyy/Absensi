using Application.Common.Dtos.Divisions;
using Application.Divisions.Commands;
using Application.Divisions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DivisionController : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<DivisionDto>>> GetDivisions()
    {
        var users = await mediator.Send(new GetDivisionList.Query());
        if (users == null || users.Count == 0)
            return NotFound("No users found.");
        return Ok(users);
    }

    [HttpGet("{IdDivision}")]
    public async Task<ActionResult<DivisionDto>> GetDivisionDetails(Guid IdDivision)
    {
       var division = await mediator.Send(new GetDivisionById.Query { IdDivision = IdDivision });
        if (division == null)
            return NotFound($"User with ID {IdDivision} not found.");
        return Ok(division);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateDivisions(CreateDivisionDto division)
    {
        if (division == null)
            return BadRequest("Division data is invalid.");
        var result = await mediator.Send(new CreateDivision.Command { Division = division });
        if (result == null)
            return BadRequest("Failed to create division.");

        return CreatedAtAction(nameof(GetDivisionDetails), new { IdDivision = result,
        message = "Division Create Success" }, result);
    }   

    [HttpPut("{IdDivision}")]
    public async Task<ActionResult> EditDivision(DivisionDto div)
    { 
        if (div == null)
            return BadRequest("leave data is invalid.");
        await mediator.Send(new EditDivision.Command { Division = div });
        return Ok(new {message = "Division updated successfully"});
    }

    [Authorize(Roles = "Leader,Admin")]
    [HttpDelete("{IdDivision}")]
    public async Task<ActionResult> DeleteDivision(Guid IdDivision)
    {
        await mediator.Send(new DeleteDivision.Command { IdDivision = IdDivision });
        return Ok(new { 
            message = "Division deleted successfully" 
        });    
    }
}
