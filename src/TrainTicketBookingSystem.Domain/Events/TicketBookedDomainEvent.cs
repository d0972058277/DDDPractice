using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Domain.Events;

public class TicketBookedDomainEvent : DomainEvent
{
    public Guid TicketId { get; }
    public Guid TrainId { get; }
    public Location From { get; }
    public Location To { get; }
    public Date Date { get; }

    public TicketBookedDomainEvent(Guid ticketId, Guid trainId, Location from, Location to,
        Date date)
    {
        TicketId = ticketId;
        TrainId = trainId;
        From = from;
        To = to;
        Date = date;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return TicketId;
        yield return TrainId;
        yield return From;
        yield return To;
        yield return @Date;
    }
}