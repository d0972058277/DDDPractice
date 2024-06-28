using System.Windows.Input;
using TrainTicketBookingSystem.Domain.Architecture;

namespace TrainTicketBookingSystem.Application.Architecture;

public interface IMediator
{
    Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;

    Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);

    Task PublishAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
        where TDomainEvent : DomainEvent;
}