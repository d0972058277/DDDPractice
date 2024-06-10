using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem;

public class Location : ValueObject
{
    public string Name { get; private set; }

    private Location(string name)
    {
        Name = name;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
    }

    public static Result<Location> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Location>("不可為空");
        }

        return new Location(name);
    }
}
