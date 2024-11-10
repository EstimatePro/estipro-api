using FluentResults;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poke.Application.DTOs.Auth0;
using Poke.Application.DTOs.Users;
using Poke.Application.Users.Commands.AddUser;
using Poke.Application.Users.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Poke.Presentation.Controllers;

public sealed class UsersController : BaseApiController<UsersController>
{
    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get user")]
    [ProducesResponseType<UserDto>(200)]
    [ProducesResponseType<Result>(400)]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    [SwaggerOperation(Summary = "Create a new user")]
    [ProducesResponseType<UserDto>(200)]
    [ProducesResponseType<UserDto>(400)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto user, CancellationToken cancellationToken)
    {
        var command = new AddUserCommand(user);
        var result = await Sender.Send(command, cancellationToken);
        return result.ToActionResult();
    }

    [AllowAnonymous]
    [HttpPost("token")]
    [SwaggerOperation(Summary = "Get access token")]
    [ProducesResponseType<AccessTokenResponse>(200)]
    [ProducesResponseType<Result>(400)]
    public async Task<IActionResult> Authorize([FromBody] AccessTokenUserCredentials tokenRequest)
    {
        var result = await Auth0Service.GetAccessToken(tokenRequest);
        return Ok(result);
    }
}