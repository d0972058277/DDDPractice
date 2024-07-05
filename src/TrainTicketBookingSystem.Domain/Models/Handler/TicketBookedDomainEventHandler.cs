using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models.Handler;

public class TicketBookedDomainEventHandler : DomainEventHandlerBase<Ticket, TicketBookedDomainEvent>
{
    public override void Handle(Ticket aggregate, TicketBookedDomainEvent domainEvent)
    {
        aggregate.SetId(domainEvent.TicketId);
        aggregate.TrainId = domainEvent.TrainId;
        aggregate.From = domainEvent.From;
        aggregate.To = domainEvent.To;
        aggregate.Date = domainEvent.Date;
        aggregate.PaymentStatus = PaymentStatus.Unpaid;
    }
}