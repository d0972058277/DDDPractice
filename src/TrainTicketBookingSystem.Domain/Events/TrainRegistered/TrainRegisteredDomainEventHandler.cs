using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Domain.Events.TrainRegistered;

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