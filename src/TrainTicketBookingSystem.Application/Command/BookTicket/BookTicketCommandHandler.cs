using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application.Command.BookTicket;

public class BookTicketCommandHandler : ICommandHandler<BookTicketCommand, Guid>
{
    private readonly ITrainRepository _trainRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;


    public BookTicketCommandHandler(ITrainRepository trainRepository, ITicketRepository ticketRepository,
        IMediator mediator)
    {
        _trainRepository = trainRepository;
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(BookTicketCommand request, CancellationToken cancellationToken)
    {
        var train = await _trainRepository.FindAsync(request.TrainId, cancellationToken);
        var ticketId = Guid.NewGuid();
        var ticket = BookTrainTicketService.Execute(train, ticketId, request.From, request.To, request.Date);
        await _trainRepository.UpdateAsync(train, cancellationToken);
        await _ticketRepository.AddAsync(ticket, cancellationToken);
        await _mediator.PublishAndClearDomainEvents(ticket, cancellationToken);
        return ticketId;
    }
}