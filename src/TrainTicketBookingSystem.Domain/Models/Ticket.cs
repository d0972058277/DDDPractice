using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;
using TrainTicketBookingSystem.Domain.Models.Handler;

namespace TrainTicketBookingSystem.Domain.Models;

public class Ticket : Aggregate<Guid>
{
    private static readonly Dictionary<Type, IDomainEventHandler> EventHandlers = new()
    {
        { typeof(TicketBookedDomainEvent), new TicketBookedDomainEventHandler() },
        { typeof(TicketPaidDomainEvent), new TicketPaidDomainEventHandler() }
    };

    private Ticket()
    {
    }

    internal void SetId(Guid id) => Id = id;
    public Guid TrainId { get; internal set; }
    public Location From { get; internal set; }
    public Location To { get; internal set; }
    public Date Date { get; internal set; }
    public PaymentStatus PaymentStatus { get; internal set; }

    public static Ticket Book(Guid id, Guid trainId, Location from, Location to, Date date)
    {
        var ticket = new Ticket();
        ticket.Apply(new TicketBookedDomainEvent(id, trainId, from, to, date));
        return ticket;
    }

    public void Pay()
    {
        Apply(new TicketPaidDomainEvent(Id));
    }

    protected override void When(DomainEvent domainEvent)
    {
        if (EventHandlers.TryGetValue(domainEvent.GetType(), out var handler))
        {
            handler.Handle(this, domainEvent);
        }
    }
}