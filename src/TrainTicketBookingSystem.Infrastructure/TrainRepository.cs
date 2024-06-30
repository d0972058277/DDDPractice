using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Infrastructure;

public class TrainRepository : ITrainRepository
{
    private readonly TrainTicketBookingSystemDbContext _dbContext;

    public TrainRepository(TrainTicketBookingSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Train> FindAsync(Guid trainId, CancellationToken cancellationToken)
    {
        var train = await _dbContext.Trains.FindAsync(new object?[] { trainId }, cancellationToken);
        return train!;
    }

    public async Task AddAsync(Train train, CancellationToken cancellationToken)
    {
        _dbContext.Trains.Add(train);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Train train, CancellationToken cancellationToken)
    {
        _dbContext.Trains.Update(train);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}