using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models;

public class Ticket : Aggregate<Guid>
{
    private Ticket(Guid id, Guid trainId, Location from, Location to, Date date, PaymentStatus paymentStatus) : base(id)
    {
        TrainId = trainId;
        From = from;
        To = to;
        Date = date;
        PaymentStatus = paymentStatus;
    }

    public Guid TrainId { get; private set; }
    public Location From { get; private set; }
    public Location To { get; private set; }
    public Date Date { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }

    public static Ticket Book(Guid id, Guid trainId, Location from, Location to, Date date)
    {
        var ticket = new Ticket(id, trainId, from, to, date, PaymentStatus.Unpaid);
        ticket.AddDomainEvent(new TicketBookedDomainEvent(ticket.Id, ticket.TrainId, ticket.From, ticket.To,
            ticket.Date));
        return ticket;
    }

    public void Pay()
    {
        if (PaymentStatus == PaymentStatus.Unpaid)
        {
            PaymentStatus = PaymentStatus.Paid;
            AddDomainEvent(new TicketPaidDomainEvent(Id));
        }
        else
        {
            throw new DomainException("已經付過錢了");
        }
    }
}