using Microsoft.AspNetCore.Mvc;

namespace TrainTicketBookingSystem.WebApi.Controllers;

public class RegisterTrainRequest
{
    public int Seats { get; set; }
    public IEnumerable<string> Locations { get; set; }
    public DateTime Date { get; set; }
}

public class RegisterTrainResponse
{
    public Guid Id { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly TrainTicketBoolingSystemDbContext _trainTicketBoolingSystemDbContext;

    public TrainController(TrainTicketBoolingSystemDbContext trainTicketBoolingSystemDbContext)
    {
        _trainTicketBoolingSystemDbContext = trainTicketBoolingSystemDbContext;
    }



    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterTrainRequest request)
    {
        var id = Guid.NewGuid();
        var train = Train.Register(id, request.Seats, request.Locations.Select(x => Location.Create(x).Value), Date.Create(request.Date).Value);
        _trainTicketBoolingSystemDbContext.Add(train);
        await _trainTicketBoolingSystemDbContext.SaveChangesAsync();
        return Ok(new RegisterTrainResponse { Id = id });
    }
}
