using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Domain.Events;

public class TrainRegisteredDomainEvent : DomainEvent
{
    public Guid TrainId { get; }
    public int Seats { get; }
    public IReadOnlyList<Location> Locations { get; }
    public Date Date { get; }

    public TrainRegisteredDomainEvent(Guid trainId, int seats, IReadOnlyList<Location> locations, Date date)
    {
        TrainId = trainId;
        Seats = seats;
        Locations = locations;
        Date = date;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return TrainId;
        yield return Seats;
        foreach (var location in Locations) yield return location;
        yield return @Date;
    }
}