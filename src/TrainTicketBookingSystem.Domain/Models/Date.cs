using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.Domain.Models;

public class Date : ValueObject
{
    private Date(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Date Create(DateTime value)
    {
        return new Date(value);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}