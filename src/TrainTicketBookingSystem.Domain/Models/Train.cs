using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models;

public class Train : Aggregate<Guid>
{
    protected Train(Guid id, int seats, List<Location> locations, Date date) : base(id)
    {
        Seats = seats;
        _locations = locations;
        Date = date;
    }

    public int Seats { get; private set; }
    private List<Location> _locations;
    public IReadOnlyList<Location> Locations => _locations.AsReadOnly();
    public Date Date { get; private set; }

    public static Train Register(Guid id, int seats, IEnumerable<Location> locations, Date date)
    {
        var train = new Train(id, seats, locations.ToList(), date);
        train.AddDomainEvent(new TrainRegisteredDomainEvent(train.Id, train.Seats, train.Locations, train.Date));
        return train;
    }

    public void BookTicket()
    {
        if (Seats == 0) throw new DomainException("票已售完");

        Seats--;
    }
}