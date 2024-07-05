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

    protected abstract IEnumerable<KeyValuePair<Type, IDomainEventHandler>> GetDomainEventHandlers();

    protected virtual void When(DomainEvent domainEvent)
    {
        var domainEventHandlers =
            GetDomainEventHandlers().ToDictionary(e => e.Key, e => e.Value);

        if (domainEventHandlers.TryGetValue(domainEvent.GetType(), out var handler))
        {
            handler.Handle(this, domainEvent);
        }
    }

    protected void Apply(DomainEvent domainEvent)
    {
        When(domainEvent);
        AddDomainEvent(domainEvent);
    }

    private void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}