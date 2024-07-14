using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TrainEndpoint;

[ApiController]
[Route("api/[controller]")]
public class TrainController : ControllerBase
{
    private readonly ITrainRepository _trainRepository;

    public TrainController(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterTrainRequest request)
    {
        var train = Train.Register(Guid.NewGuid(), request.Seats,
            request.Locations.Select(Location.Create),
            Date.Create(request.Date));
        await _trainRepository.AddAsync(train);
        return Ok(new RegisterTrainResponse { Id = train.Id });
    }
}