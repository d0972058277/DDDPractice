using TrainTicketBookingSystem.MDD;

namespace TrainTicketBookingSystem;

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

    public static Train Register(Guid id, int seats, List<Location> locations, Date date)
    {
        return new Train(id, seats, locations.ToList(), date);
    }
}
