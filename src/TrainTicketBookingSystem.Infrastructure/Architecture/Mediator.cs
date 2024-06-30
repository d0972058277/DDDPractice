using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Domain.Architecture;

namespace TrainTicketBookingSystem.Infrastructure.Architecture;

public class Mediator : IMediator
{
    private readonly MediatR.IMediator _mediator;

    public Mediator(MediatR.IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command,
        CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent,
        CancellationToken cancellationToken = default) where TDomainEvent : DomainEvent
    {
        return _mediator.Publish(domainEvent, cancellationToken);
    }
}