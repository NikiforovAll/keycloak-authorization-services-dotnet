namespace Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? mediator;

    protected ISender Mediator =>
        this.mediator ??=
            this.HttpContext.RequestServices.GetRequiredService<ISender>();
}
