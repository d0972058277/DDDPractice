using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models;

public class TrainRegisteredDomainEventHandler : DomainEventHandlerBase<Train, TrainRegisteredDomainEvent>
{
    public override void Handle(Train aggregate, TrainRegisteredDomainEvent domainEvent)
    {
        aggregate.SetId(domainEvent.TrainId);
        aggregate.SetLocations(domainEvent.Locations);
        aggregate.Seats = domainEvent.Seats;
        aggregate.Date = domainEvent.Date;
    }
}