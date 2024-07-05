using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.Domain.Architecture;

public abstract class Aggregate<TId> : Entity<TId>, IAggregateRoot where TId : IComparable<TId>
{
    protected Aggregate() : base()
    {
        _domainEventHandlers = new Dictionary<Type, IDomainEventHandler>();
    }

    protected Aggregate(TId id) : base(id)
    {
        _domainEventHandlers = new Dictionary<Type, IDomainEventHandler>();
    }

    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private readonly Dictionary<Type, IDomainEventHandler> _domainEventHandlers;
    protected void RegisterDomainEventHandler<TEvent>(IDomainEventHandler handler) where TEvent : DomainEvent
    {
        _domainEventHandlers[typeof(TEvent)] = handler;
    }

    private void When(DomainEvent domainEvent)
    {
        if (_domainEventHandlers.TryGetValue(domainEvent.GetType(), out var handler))
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