using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Infrastructure;

public class TicketRepository : ITicketRepository
{
    private readonly TrainTicketBookingSystemDbContext _dbContext;

    public TicketRepository(TrainTicketBookingSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        _dbContext.Tickets.Add(ticket);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Ticket> FindAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        return (await _dbContext.Tickets.FindAsync(new object?[] { ticketId }, cancellationToken))!;
    }

    public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken)
    {
        _dbContext.Tickets.Update(ticket);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}