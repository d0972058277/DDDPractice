namespace TrainTicketBookingSystem.WebApi.Controllers.TrainEndpoint;

public class RegisterTrainRequest
{
    public int Seats { get; init; }
    public IEnumerable<string> Locations { get; init; }
    public DateTime Date { get; init; }
}