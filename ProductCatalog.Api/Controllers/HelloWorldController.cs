using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult SayHello() => Ok("Hello World!");
}
