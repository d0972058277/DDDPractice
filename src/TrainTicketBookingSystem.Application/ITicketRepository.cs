using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Application;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<Ticket> FindAsync(Guid ticketId, CancellationToken cancellationToken);
    Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken);
}