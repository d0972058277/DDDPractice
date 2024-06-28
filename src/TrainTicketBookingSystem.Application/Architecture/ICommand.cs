using MediatR;

namespace TrainTicketBookingSystem.Application.Architecture;

public interface IBaseCommand
{
}

public interface ICommand<out TResult> : IBaseCommand, IRequest<TResult>
{
}

public interface ICommand : IBaseCommand, IRequest
{
}