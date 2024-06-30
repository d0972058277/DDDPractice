using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Application.Command.RegisterTrain;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TrainEndpoint;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterTrainRequest request)
    {
        var command = new RegisterTrainCommand(
            request.Seats,
            request.Locations.Select(Location.Create),
            Date.Create(request.Date));
        var trainId = await _mediator.ExecuteAsync(command);
        return Ok(new RegisterTrainResponse { Id = trainId });
    }
}