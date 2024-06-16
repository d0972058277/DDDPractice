using Microsoft.AspNetCore.Mvc;

namespace TrainTicketBookingSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    [HttpPost]
    public IActionResult Register()
    {
        return Ok();
    }
}
