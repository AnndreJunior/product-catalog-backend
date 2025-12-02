using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Api.HelloWorld;

[Route("[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult HelloWorld() => Ok("Hello World!");
}
