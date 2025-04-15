using Application.Common.Dtos.Users;
using Application.Users.Commands;
using Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        var users = await mediator.Send(new GetUserList.Query());
        if (users == null || users.Count == 0)
            return NotFound("No users found.");
        return Ok(users);
    }

    [HttpGet("{IdUser}")]
    public async Task<ActionResult<UserDto>> GetUserDetail(Guid IdUser)
    {
        var user = await mediator.Send(new GetUserById.Query { IdUser = IdUser });
        if (user == null)
            return NotFound($"User with ID {IdUser} not found.");
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateUsers(CreateUserDto user)
    {
        if (user == null)
            return BadRequest("User data is invalid.");
        var result = await mediator.Send(new CreateUser.Command { User = user });
        
        if (result == null)
            return BadRequest("Failed to create user.");

        return CreatedAtAction(nameof(GetUserDetail), new { IdUser = result }, result); 
    }

    [HttpPut("{IdUser}")]
    public async Task<ActionResult> EditUser(EditUserDto user)
    { 
        if (user == null)
            return BadRequest("User data is invalid.");
        await mediator.Send(new EditUser.Command { User = user });
        return NoContent();
    }

    [Authorize(Roles = "Leader,Admin")]
    [HttpDelete("{idUser}")]
    public async Task<ActionResult> DeleteUser(Guid IdUser)
    {
        await mediator.Send(new DeleteUser.Command { IdUser = IdUser });
        return Ok();    
    }
}
