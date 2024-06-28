using TrainTicketBookingSystem.Domain.Architecture;

namespace TrainTicketBookingSystem.Application.Architecture;

public static class MediatorExtensions
{
    public static async Task PublishAsync(this IMediator mediator, IEnumerable<DomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await mediator.PublishAsync(domainEvent, cancellationToken);
        }
    }

    public static async Task PublishAndClearDomainEvents<TId>(this IMediator mediator, Aggregate<TId> aggregateRoot,
        CancellationToken cancellationToken = default) where TId : IComparable<TId>
    {
        // NOTE: 由於是 call by reference ，如果不透過 ToList 複製，下一段 ClearDomainEvents 會將 DomainEvents 清除
        var domainEvents = aggregateRoot.DomainEvents.ToList();

        aggregateRoot.ClearDomainEvents();

        await mediator.PublishAsync(domainEvents, cancellationToken);
    }
}