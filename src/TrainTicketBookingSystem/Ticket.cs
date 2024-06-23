using TrainTicketBookingSystem.Architecture;
using TrainTicketBookingSystem.Exceptions;

namespace TrainTicketBookingSystem;

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
        return new Ticket(id, trainId, from, to, date, PaymentStatus.Unpaid);
    }

    public void Pay()
    {
        if (PaymentStatus == PaymentStatus.Unpaid)
        {
            PaymentStatus = PaymentStatus.Paid;
        }
        else
        {
            throw new DomainException("已經付過錢了");
        }
    }
}
