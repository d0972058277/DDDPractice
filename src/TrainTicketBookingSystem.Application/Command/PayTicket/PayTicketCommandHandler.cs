using TrainTicketBookingSystem.Application.Architecture;

namespace TrainTicketBookingSystem.Application.Command.PayTicket;

public class PayTicketCommandHandler : ICommandHandler<PayTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;

    public PayTicketCommandHandler(ITicketRepository ticketRepository, IMediator mediator)
    {
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    public async Task Handle(PayTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.FindAsync(request.TicketId, cancellationToken);
        ticket.Pay();
        await _ticketRepository.UpdateAsync(ticket, cancellationToken);
        await _mediator.PublishAndClearDomainEvents(ticket, cancellationToken);
    }
}