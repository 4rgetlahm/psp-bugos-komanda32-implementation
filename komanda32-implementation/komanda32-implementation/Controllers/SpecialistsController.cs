using Microsoft.AspNetCore.Mvc;

namespace komanda32_implementation.Controllers;

[ApiController]
public class SpecialistsController : Controller
{
    [HttpGet]
    [Route("employees")]
    public IActionResult Get()
    {
        return Ok();
    }
}
// I want to see my calendar, so I can plan my work.
// I want to select a specific appointment, so I can read the description and know the specifics of a
// service I have to provide.