using EstiPro.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstiPro.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class BaseApiController<T> : ControllerBase
{
    protected ISender Sender => HttpContext.RequestServices.GetService<ISender>()!;
    protected ILogger<T> Logger => HttpContext.RequestServices.GetService<ILogger<T>>()!;
    protected IAuth0Service Auth0Service => HttpContext.RequestServices.GetService<IAuth0Service>()!;
}
