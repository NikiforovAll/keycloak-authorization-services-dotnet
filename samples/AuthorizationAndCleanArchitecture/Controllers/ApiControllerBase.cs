namespace Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected ISender Mediator =>
        field ??= this.HttpContext.RequestServices.GetRequiredService<ISender>();
}
