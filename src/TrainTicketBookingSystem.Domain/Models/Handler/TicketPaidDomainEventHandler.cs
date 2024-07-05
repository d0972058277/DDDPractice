using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models.Handler;

public class TicketPaidDomainEventHandler : DomainEventHandlerBase<Ticket, TicketPaidDomainEvent>
{
    public override void Handle(Ticket aggregate, TicketPaidDomainEvent domainEvent)
    {
        if (aggregate.PaymentStatus == PaymentStatus.Unpaid)
        {
            aggregate.PaymentStatus = PaymentStatus.Paid;
        }
        else
        {
            throw new DomainException("已經付過錢了");
        }
    }
}