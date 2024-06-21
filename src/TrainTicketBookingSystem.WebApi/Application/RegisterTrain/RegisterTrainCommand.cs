namespace TrainTicketBookingSystem.WebApi.Application.RegisterTrain;

public class RegisterTrainCommand
{
    public int Seats { get; }
    public IEnumerable<string> Locations { get; }
    public DateTime DateTime { get; }

    public RegisterTrainCommand(int seats, IEnumerable<string> locations, DateTime dateTime)
    {
        Seats = seats;
        Locations = locations;
        DateTime = dateTime;
    }
}