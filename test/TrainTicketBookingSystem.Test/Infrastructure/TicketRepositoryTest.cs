using FluentAssertions;
using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Domain.Models;
using TrainTicketBookingSystem.Infrastructure;

namespace TrainTicketBookingSystem.Test.Infrastructure;

public class TicketRepositoryTest
{
    private readonly ITicketRepository _ticketRepository;
    private readonly TrainTicketBookingSystemDbContext _dbContext;

    public TicketRepositoryTest(ITicketRepository ticketRepository, TrainTicketBookingSystemDbContext dbContext)
    {
        _ticketRepository = ticketRepository;
        _dbContext = dbContext;
    }

    [Fact]
    public async Task FindAsync()
    {
        // Given
        var ticket = BookATicket();
        _dbContext.Tickets.Add(ticket);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        // When
        var actual = await _ticketRepository.FindAsync(ticket.Id, default);

        // Then
        ActualShouldBeExpect(actual, ticket);
    }

    [Fact]
    public async Task AddAsync()
    {
        // Given
        var ticket = BookATicket();

        // When
        await _ticketRepository.AddAsync(ticket, default);
        _dbContext.ChangeTracker.Clear();

        // Then
        var actual = await _dbContext.Tickets.FindAsync(ticket.Id);
        ActualShouldBeExpect(actual, ticket);
    }

    [Fact]
    public async Task UpdateAsync()
    {
        // Given
        var original = BookATicket();
        await AddTicketAsync(original);
        var ticket = await FindTicketAsync(original.Id);
        ticket.Pay();

        // When
        await _ticketRepository.UpdateAsync(ticket, default);
        _dbContext.ChangeTracker.Clear();

        // Then
        var actual = await _dbContext.Tickets.FindAsync(ticket.Id);
        ActualShouldBeExpect(actual, ticket);
    }

    private async Task AddTicketAsync(Ticket original)
    {
        _dbContext.Tickets.Add(original);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();
    }

    private async Task<Ticket> FindTicketAsync(Guid ticketId)
    {
        return (await _dbContext.Tickets.FindAsync(ticketId))!;
    }

    private static Ticket BookATicket()
    {
        return Ticket.Book(Guid.NewGuid(), Guid.NewGuid(), Location.Create("taipei"), Location.Create("taichung"),
            Date.Create(DateTime.UtcNow));
    }

    private static void ActualShouldBeExpect(Ticket? actual, Ticket expect)
    {
        actual.Should().NotBeNull();
        actual!.Id.Should().Be(expect.Id);
        actual.TrainId.Should().Be(expect.TrainId);
        actual.From.Should().Be(expect.From);
        actual.To.Should().Be(expect.To);
        actual.PaymentStatus.Should().Be(expect.PaymentStatus);
        actual.Date.Should().Be(expect.Date);
    }
}