namespace TrainTicketBookingSystem.Domain.Models;

public static class BookTrainTicketService
{
    public static Ticket Execute(Train train, Guid id, Location from, Location to, Date date)
    {
        train.BookTicket();
        return Ticket.Book(id, train.Id, from, to, date);
    }
}