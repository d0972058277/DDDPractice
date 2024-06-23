namespace TrainTicketBookingSystem.Domain.Architecture;

public class DomainException : Exception
{
    public DomainException(string? message) : base(message)
    {
    }
}