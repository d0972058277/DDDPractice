using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application.Command.BookTicket;

public class BookTicketCommand : ICommand<Guid>
{
    public readonly Guid TrainId;
    public readonly Location From;
    public readonly Location To;
    public readonly Date Date;

    public BookTicketCommand(Guid trainId, Location from, Location to, Date date)
    {
        TrainId = trainId;
        From = from;
        To = to;
        Date = date;
    }
}