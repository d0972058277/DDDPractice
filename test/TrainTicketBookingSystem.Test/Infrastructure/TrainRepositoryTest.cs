using System.Diagnostics;
using FluentAssertions;
using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Domain.Models;
using TrainTicketBookingSystem.Infrastructure;

namespace TrainTicketBookingSystem.Test.Infrastructure;

public class TrainRepositoryTest
{
    private readonly ITrainRepository _trainRepository;
    private readonly TrainTicketBookingSystemDbContext _dbContext;

    public TrainRepositoryTest(ITrainRepository trainRepository, TrainTicketBookingSystemDbContext dbContext)
    {
        _trainRepository = trainRepository;
        _dbContext = dbContext;
    }

    [Fact]
    public async Task FindAsync()
    {
        // Given
        var train = RegisterATrain();
        _dbContext.Trains.Add(train);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        // When
        var actual = await _trainRepository.FindAsync(train.Id, default);

        // Then
        actual.Id.Should().Be(train.Id);
        actual.Seats.Should().Be(train.Seats);
        actual.Locations.Should().BeEquivalentTo(train.Locations);
        actual.Date.Should().Be(train.Date);
    }

    [Fact]
    public async Task AddAsync()
    {
        // Given
        var train = RegisterATrain();

        // When
        await _trainRepository.AddAsync(train, default);
        _dbContext.ChangeTracker.Clear();

        // Then
        var actual = await _dbContext.Trains.FindAsync(train.Id);
        actual.Should().NotBeNull();
        actual!.Id.Should().Be(train.Id);
        actual.Seats.Should().Be(train.Seats);
        actual.Locations.Should().BeEquivalentTo(train.Locations);
        actual.Date.Should().Be(train.Date);
    }

    [Fact]
    public async Task UpdateAsync()
    {
        // Given
        var original = RegisterATrain();
        await AddTrainAsync(original);
        var train = await FindTrainAsync(original.Id);
        train.BookTicket();

        // When
        await _trainRepository.UpdateAsync(train, default);
        _dbContext.ChangeTracker.Clear();

        // Then
        var actual = await _dbContext.Trains.FindAsync(train.Id);
        actual.Should().NotBeNull();
        actual!.Id.Should().Be(train.Id);
        actual.Seats.Should().Be(train.Seats);
        actual.Locations.Should().BeEquivalentTo(train.Locations);
        actual.Date.Should().Be(train.Date);
    }

    private async Task<Train> FindTrainAsync(Guid trainId)
    {
        return (await _dbContext.Trains.FindAsync(trainId))!;
    }

    private async Task AddTrainAsync(Train train)
    {
        _dbContext.Trains.Add(train);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
    }

    private static Train RegisterATrain()
    {
        var train = Train.Register(Guid.NewGuid(), 10,
            new List<Location> { Location.Create("taipei"), Location.Create("taichung") },
            Date.Create(DateTime.UtcNow));
        return train;
    }
}