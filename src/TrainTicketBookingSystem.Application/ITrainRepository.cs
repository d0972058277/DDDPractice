using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application;

public interface ITrainRepository
{
    Task<Train> FindAsync(Guid trainId, CancellationToken cancellationToken);
    Task AddAsync(Train train, CancellationToken cancellationToken);
    Task UpdateAsync(Train train, CancellationToken cancellationToken);
}