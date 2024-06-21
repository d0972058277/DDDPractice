using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.WebApi.Application.RegisterTrain;

namespace TrainTicketBookingSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly RegisterTrainCommandHandler _registerTrainCommandHandler;

    public TrainController(TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _registerTrainCommandHandler = new RegisterTrainCommandHandler(trainTicketBookingSystemDbContext);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterTrainRequest request)
    {
        var command = new RegisterTrainCommand(request.Seats, request.Locations, request.Date);
        var id = await _registerTrainCommandHandler.Handle(command);
        return Ok(new RegisterTrainResponse { Id = id });
    }
}