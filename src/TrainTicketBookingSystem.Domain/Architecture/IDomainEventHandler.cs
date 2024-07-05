namespace TrainTicketBookingSystem.Domain.Architecture;

public interface IDomainEventHandler
{
    void Handle(IAggregateRoot aggregate, DomainEvent domainEvent);
}