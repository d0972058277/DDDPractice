using CSharpFunctionalExtensions;
using TrainTicketBookingSystem.Domain.Architecture;

namespace TrainTicketBookingSystem.Domain.Models;

public class Location : ValueObject
{
    private Location(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static Location Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("名字不可為空");

        return new Location(name);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Name;
    }
}