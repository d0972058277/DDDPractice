namespace TrainTicketBookingSystem.WebApi.Controllers;

public class RegisterTrainRequest
{
    public int Seats { get; init; }
    public IEnumerable<string> Locations { get; init; }
    public DateTime Date { get; init; }
}