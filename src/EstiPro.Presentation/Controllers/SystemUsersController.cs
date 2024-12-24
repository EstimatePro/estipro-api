using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EstiPro.Application.DTOs.Users;
using EstiPro.Application.Users.Commands.VerifyUser;

namespace EstiPro.Presentation.Controllers;

[Authorize(AuthenticationSchemes = "Basic")]
public class SystemUsersController : BaseApiController<UsersController>
{
    [HttpPost("create")]
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