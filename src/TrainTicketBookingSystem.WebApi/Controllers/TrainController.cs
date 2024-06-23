using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.WebApi.Application.RegisterTrain;

namespace TrainTicketBookingSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly RegisterTrainCommandHandler _registerTrainCommandHandler;

    public TrainController(RegisterTrainCommandHandler registerTrainCommandHandler)
    {
        _registerTrainCommandHandler = registerTrainCommandHandler;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterTrainRequest request)
    {
        var command = new RegisterTrainCommand(request.Seats, request.Locations, request.Date);
        var id = await _registerTrainCommandHandler.HandleAsync(command);
        return Ok(new RegisterTrainResponse { Id = id });
    }
}