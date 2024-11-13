using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poke.Application.DTOs.Users;
using Poke.Application.Users.Commands.VerifyUser;
using Swashbuckle.AspNetCore.Annotations;

namespace Poke.Presentation.Controllers;

[Authorize(AuthenticationSchemes = "Basic")]
public class SystemUsersController : BaseApiController<UsersController>
{
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Create new user")]
    [ProducesResponseType<UserDto>(200)]
    [ProducesResponseType<UserDto>(400)]
    public async Task<IActionResult> CreateNewAuth0User([FromBody] Auth0UserRegistrationDto? newUser)
    {
        if (newUser == null
            || newUser.UserId == Guid.Empty)
        {
            return BadRequest("Invalid data.");
        }

        var command = new CreateUserCommand(newUser);
        var result = await Sender.Send(command, CancellationToken.None);

        if (!result.IsSuccess)
        {
            return Ok(new { message = "User creation failed." });
        }

        Logger.LogInformation("New user created: {UserId}, {Email}, {Name}", newUser.UserId, newUser.Email,
            newUser.Name);

        return Ok(new { message = "User created." });
    }
}