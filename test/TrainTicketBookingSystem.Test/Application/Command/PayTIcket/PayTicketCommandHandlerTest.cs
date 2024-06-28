using Moq;
using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Application.Command.BookTicket;
using TrainTicketBookingSystem.Application.Command.PayTicket;
using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Test.Application.Command.PayTicket;

public class PayTicketCommandHandlerTest
{
    [Fact]
    public async Task Handle()
    {
        // Given
        var ticketRepository = new Mock<ITicketRepository>();
        var mediator = new Mock<IMediator>();

        var ticket = Ticket.Book(Guid.NewGuid(), Guid.NewGuid(), Location.Create("taipei"), Location.Create("taichung"),
            Date.Create(DateTime.UtcNow));
        ticketRepository.Setup(r => r.FindAsync(ticket.Id, default)).ReturnsAsync(ticket);

        var command = new PayTicketCommand(ticket.Id);
        var handler = new PayTicketCommandHandler(ticketRepository.Object, mediator.Object);

        // When
        await handler.Handle(command, default);

        // Then
        ticketRepository.Verify(r => r.UpdateAsync(It.Is<Ticket>(t => t.Id == ticket.Id), default));
        mediator.Verify(x => x.PublishAsync(It.Is<DomainEvent>(e => e is TicketPaidDomainEvent), default));
    }
}