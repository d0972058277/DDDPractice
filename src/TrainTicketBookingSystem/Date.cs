using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem;

public class Date : ValueObject
{
    public DateTime Value { get; private set; }

    private Date(DateTime value)
    {
        Value = value;
    }

    public static Result<Date> Create(DateTime value)
    {
        return new Date(value);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
