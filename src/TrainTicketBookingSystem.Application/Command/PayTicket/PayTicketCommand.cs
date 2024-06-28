using TrainTicketBookingSystem.Application.Architecture;

namespace TrainTicketBookingSystem.Application.Command.PayTicket;

public class PayTicketCommand : ICommand
{
    public readonly Guid TicketId;

    public PayTicketCommand(Guid ticketId)
    {
        TicketId = ticketId;
    }
}