using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.Domain.Architecture;

public class Aggregate<TId> : Entity<TId> where TId : IComparable<TId>
{
    protected Aggregate() : base()
    {
    }

    protected Aggregate(TId id) : base(id)
    {
    }

    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}