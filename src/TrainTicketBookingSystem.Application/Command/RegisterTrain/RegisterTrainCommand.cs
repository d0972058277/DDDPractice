using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application.Command.RegisterTrain;

public class RegisterTrainCommand : ICommand<Guid>
{
    public readonly int Seats;
    public readonly IEnumerable<Location> Locations;
    public readonly Date Date;

    public RegisterTrainCommand(int seats, IEnumerable<Location> locations, Date date)
    {
        Seats = seats;
        Locations = locations;
        Date = date;
    }
}