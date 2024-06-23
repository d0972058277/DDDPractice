namespace TrainTicketBookingSystem;

public interface ITrainRepository
{
    Task AddAsync(Train train);
    Task<Train?> FindAsync(Guid trainId);
}