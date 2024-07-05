using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.Domain.Architecture;

public abstract class Aggregate<TId> : Entity<TId>, IAggregateRoot where TId : IComparable<TId>
{
    protected Aggregate() : base()
    {
    }

    protected Aggregate(TId id) : base(id)
    {
    }

    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected abstract void When(DomainEvent domainEvent);

    protected void Apply(DomainEvent domainEvent)
    {
        When(domainEvent);
        AddDomainEvent(domainEvent);
    }

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}