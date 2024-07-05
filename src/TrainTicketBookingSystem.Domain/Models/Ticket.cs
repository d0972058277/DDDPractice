using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;

namespace TrainTicketBookingSystem.Domain.Models;

public class Ticket : Aggregate<Guid>
{
    private Ticket()
    {
    }

    public Guid TrainId { get; private set; }
    public Location From { get; private set; }
    public Location To { get; private set; }
    public Date Date { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }

    public static Ticket Book(Guid id, Guid trainId, Location from, Location to, Date date)
    {
        var ticket = new Ticket();
        ticket.Apply(new TicketBookedDomainEvent(id, trainId, from, to, date));
        return ticket;
    }

    public void Pay()
    {
        Apply(new TicketPaidDomainEvent(Id));
    }

    protected override void When(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case TicketBookedDomainEvent e:
                Id = e.TicketId;
                TrainId = e.TrainId;
                From = e.From;
                To = e.To;
                Date = e.Date;
                break;
            case TicketPaidDomainEvent e:
                if (PaymentStatus == PaymentStatus.Unpaid)
                {
                    PaymentStatus = PaymentStatus.Paid;
                }
                else
                {
                    throw new DomainException("已經付過錢了");
                }

                break;
        }
    }
}