using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.Domain.Models;

public class Date : ValueObject
{
    private Date()
    {
    }

    private Date(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; private set; }

    public static Date Create(DateTime value)
    {
        var truncatedToMillisecond = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute,
            value.Second, value.Millisecond);
        return new Date(truncatedToMillisecond);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}