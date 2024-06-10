using TrainTicketBookingSystem.Exceptions;

namespace TrainTicketBookingSystem;

public static class BookTrainTicketService
{
    public static Ticket Execute(Train train, Guid id, Location from, Location to, Date date)
    {
        if (train.Seats == 0)
        {
            throw new DomainException("火車已滿");
        }

        return Ticket.Book(id, train.Id, from, to, date);
    }
}
