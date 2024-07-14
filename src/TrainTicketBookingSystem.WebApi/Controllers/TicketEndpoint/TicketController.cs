using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TicketEndpoint;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITrainRepository _trainRepository;

    public TicketController(ITicketRepository ticketRepository, ITrainRepository trainRepository)
    {
        _ticketRepository = ticketRepository;
        _trainRepository = trainRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Book([FromBody] BookTicketRequest request)
    {
        var train = await _trainRepository.FindAsync(request.TrainId);
        var ticket = BookTrainTicketService.Execute(train!, Guid.NewGuid(), Location.Create(request.From),
            Location.Create(request.To), Date.Create(request.Date));
        await _ticketRepository.AddAsync(ticket);
        await _trainRepository.UpdateAsync(train!);
        return Ok(new BookTicketResponse { Id = ticket.Id });
    }

    [HttpPost("{id:guid}/Pay")]
    public async Task<ActionResult> Pay([FromRoute] Guid id)
    {
        var ticket = await _ticketRepository.FindAsync(id);
        ticket!.Pay();
        await _ticketRepository.UpdateAsync(ticket);
        return Ok();
    }
}