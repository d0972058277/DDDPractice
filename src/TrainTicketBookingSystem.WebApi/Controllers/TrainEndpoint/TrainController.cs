using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TrainEndpoint;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly TrainTicketBookingSystemDbContext _trainTicketBookingSystemDbContext;

    public TrainController(TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _trainTicketBookingSystemDbContext = trainTicketBookingSystemDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterTrainRequest request)
    {
        var train = Train.Register(Guid.NewGuid(), request.Seats,
            request.Locations.Select(Location.Create),
            Date.Create(request.Date));
        _trainTicketBookingSystemDbContext.Trains.Add(train);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return Ok(new RegisterTrainResponse { Id = train.Id });
    }
}