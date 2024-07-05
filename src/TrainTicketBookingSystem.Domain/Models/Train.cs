using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models;

public class Train : Aggregate<Guid>
{
    private Train()
    {
    }

    public int Seats { get; private set; }
    private List<Location> _locations;
    public IReadOnlyList<Location> Locations => _locations.AsReadOnly();
    public Date Date { get; private set; }

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

    protected override void When(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case TrainRegisteredDomainEvent e:
                Id = e.TrainId;
                Seats = e.Seats;
                _locations = e.Locations.ToList();
                Date = e.Date;
                break;
        }
    }
}