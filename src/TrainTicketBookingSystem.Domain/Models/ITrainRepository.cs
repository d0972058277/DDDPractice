namespace TrainTicketBookingSystem.Domain.Models;

public interface ITrainRepository
{
    Task<Train> FindAsync(Guid trainId, CancellationToken cancellationToken = default);
    Task AddAsync(Train train, CancellationToken cancellationToken = default);
    Task UpdateAsync(Train train, CancellationToken cancellationToken = default);
}