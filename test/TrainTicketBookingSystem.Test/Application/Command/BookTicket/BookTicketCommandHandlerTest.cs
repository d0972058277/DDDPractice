using System.Runtime.InteropServices.JavaScript;
using Moq;
using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Application.Command.BookTicket;
using TrainTicketBookingSystem.Application.Command.RegisterTrain;
using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Test.Application.Command.BookTicket;

public class BookTicketCommandHandlerTest
{
    [Fact]
    public async Task Handle()
    {
        // Given
        var train = Train.Register(Guid.NewGuid(), 100, [], Date.Create(DateTime.UtcNow));
        var trainRepository = new Mock<ITrainRepository>();
        trainRepository.Setup(r => r.FindAsync(train.Id, default)).ReturnsAsync(train);

        var ticketRepository = new Mock<ITicketRepository>();

        var mediator = new Mock<IMediator>();

        var command = new BookTicketCommand(train.Id, Location.Create("taipei"), Location.Create("taichung"),
            Date.Create(DateTime.UtcNow));
        var handler = new BookTicketCommandHandler(trainRepository.Object, ticketRepository.Object, mediator.Object);

        // When
        var ticketId = await handler.Handle(command, default);

        // Then
        trainRepository.Verify(x => x.UpdateAsync(It.Is<Train>(t => t.Id == train.Id), default));
        ticketRepository.Verify(x => x.AddAsync(It.Is<Ticket>(t => t.Id == ticketId), default));
        mediator.Verify(x => x.PublishAsync(It.Is<DomainEvent>(e => e is TicketBookedDomainEvent), default));
    }
}