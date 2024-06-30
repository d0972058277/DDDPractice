using Microsoft.AspNetCore.Mvc;
using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Application.Command.BookTicket;
using TrainTicketBookingSystem.Application.Command.PayTicket;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TicketEndpoint;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;


    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Book([FromBody] BookTicketRequest request)
    {
        var command = new BookTicketCommand(
            request.TrainId,
            Location.Create(request.From),
            Location.Create(request.To),
            Date.Create(request.Date));
        var ticketId = await _mediator.ExecuteAsync(command);
        return Ok(new BookTicketResponse { Id = ticketId });
    }

    [HttpPost("{id:guid}/Pay")]
    public async Task<ActionResult> Pay([FromRoute] Guid id)
    {
        var command = new PayTicketCommand(id);
        await _mediator.ExecuteAsync(command);
        return Ok();
    }
}