using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;
using TrainTicketBookingSystem.Domain.Models.Handler;

namespace TrainTicketBookingSystem.Domain.Models;

public class Train : Aggregate<Guid>
{
    private Train()
    {
        _locations = new List<Location>();
        RegisterDomainEventHandlers();
    }

    internal void SetId(Guid id) => Id = id;
    public int Seats { get; internal set; }
    internal void SetLocations(IEnumerable<Location> locations) => _locations = locations.ToList();
    private List<Location> _locations;
    public IReadOnlyList<Location> Locations => _locations.AsReadOnly();
    public Date Date { get; internal set; }

    public static Train Register(Guid id, int seats, IEnumerable<Location> locations, Date date)
    {
        var train = new Train();
        train.Apply(new TrainRegisteredDomainEvent(id, seats, locations.ToList(), date));
        return train;
    }

    public void BookTicket()
    {
        if (Seats == 0) throw new DomainException("票已售完");

        Seats--;
    }

    private void RegisterDomainEventHandlers()
    {
        RegisterDomainEventHandler<TrainRegisteredDomainEvent>(new TrainRegisteredDomainEventHandler());
    }
}