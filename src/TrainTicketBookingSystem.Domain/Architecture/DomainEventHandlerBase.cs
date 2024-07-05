namespace TrainTicketBookingSystem.Domain.Architecture;

public abstract class
    DomainEventHandlerBase<TAggregateRoot, TDomainEvent> : IDomainEventHandler
    where TAggregateRoot : IAggregateRoot
    where TDomainEvent : DomainEvent
{
    public abstract void Handle(TAggregateRoot aggregate, TDomainEvent domainEvent);

    void IDomainEventHandler.Handle(IAggregateRoot aggregate, DomainEvent domainEvent)
    {
        Handle((TAggregateRoot)aggregate, (TDomainEvent)domainEvent);
    }
}