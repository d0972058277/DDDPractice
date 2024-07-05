using TrainTicketBookingSystem.Domain.Architecture;

namespace TrainTicketBookingSystem.Domain.Events.TicketPaid;

public class TicketPaidDomainEvent : DomainEvent
{
    public Guid TicketId { get; }

    public TicketPaidDomainEvent(Guid ticketId)
    {
        TicketId = ticketId;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return TicketId;
    }
}