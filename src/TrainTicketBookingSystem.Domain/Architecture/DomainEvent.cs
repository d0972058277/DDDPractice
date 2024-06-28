using CSharpFunctionalExtensions;
using MediatR;

namespace TrainTicketBookingSystem.Domain.Architecture;

public abstract class DomainEvent : ValueObject, INotification
{
}