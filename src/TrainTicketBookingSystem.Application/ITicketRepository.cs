using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
}