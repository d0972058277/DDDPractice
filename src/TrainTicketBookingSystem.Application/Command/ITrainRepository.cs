using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application.Command;

public interface ITrainRepository
{
    Task AddAsync(Train train);
}