using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.MDD;

public class Aggregate<TId> : Entity<TId> where TId : IComparable<TId>
{
    protected Aggregate() : base() { }
    protected Aggregate(TId id) : base(id) { }

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
