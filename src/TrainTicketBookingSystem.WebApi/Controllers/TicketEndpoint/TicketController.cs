using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TicketEndpoint;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly TrainTicketBookingSystemDbContext _trainTicketBookingSystemDbContext;

    public TicketController(TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _trainTicketBookingSystemDbContext = trainTicketBookingSystemDbContext;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Book([FromBody] BookTicketRequest request)
    {
        var train = await _trainTicketBookingSystemDbContext.Trains.FindAsync(request.TrainId);
        var ticket = BookTrainTicketService.Execute(train!, Guid.NewGuid(), Location.Create(request.From),
            Location.Create(request.To), Date.Create(request.Date));
        _trainTicketBookingSystemDbContext.Tickets.Add(ticket);
        _trainTicketBookingSystemDbContext.Trains.Update(train!);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return Ok(new BookTicketResponse { Id = ticket.Id });
    }

    [HttpPost("{id:guid}/pay")]
    public async Task<ActionResult> Pay([FromRoute] Guid id)
    {
        var ticket = await _trainTicketBookingSystemDbContext.Tickets.FindAsync(id);
        ticket!.Pay();
        _trainTicketBookingSystemDbContext.Tickets.Update(ticket);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return Ok();
    }
}