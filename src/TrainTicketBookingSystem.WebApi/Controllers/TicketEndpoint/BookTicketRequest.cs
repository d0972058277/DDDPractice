using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.WebApi.Controllers.TicketEndpoint;

public class BookTicketRequest
{
    public Guid TrainId { get; init; }
    public string From { get; init; }
    public string To { get; init; }
    public DateTime Date { get; init; }
}