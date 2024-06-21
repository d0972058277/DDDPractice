using Microsoft.AspNetCore.Mvc;

namespace TrainTicketBookingSystem.WebApi.Controllers;

public class RegisterTrainRequest
{
    public int Seats { get; init; }
    public IEnumerable<string> Locations { get; init; }
    public DateTime Date { get; init; }
}

public class RegisterTrainResponse
{
    public Guid Id { get; init; }
}

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
        var id = Guid.NewGuid();
        var train = Train.Register(id, request.Seats, request.Locations.Select(x => Location.Create(x).Value), Date.Create(request.Date).Value);
        _trainTicketBookingSystemDbContext.Add(train);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return Ok(new RegisterTrainResponse { Id = id });
    }
}
