using CSharpFunctionalExtensions;

namespace TrainTicketBookingSystem.WebApi.Infrastructure;

public class TrainRepository : ITrainRepository
{
    private readonly TrainTicketBookingSystemDbContext _trainTicketBookingSystemDbContext;

    public TrainRepository(TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _trainTicketBookingSystemDbContext = trainTicketBookingSystemDbContext;
    }

    public async Task AddAsync(Train train)
    {
        _trainTicketBookingSystemDbContext.Add(train);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
    }

    public async Task<Train?> FindAsync(Guid trainId)
    {
        return await _trainTicketBookingSystemDbContext.Trains.FindAsync(trainId);
    }
}