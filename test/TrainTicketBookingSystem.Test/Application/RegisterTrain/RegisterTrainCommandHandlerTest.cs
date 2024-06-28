using Moq;
using TrainTicketBookingSystem.Application.Architecture;
using TrainTicketBookingSystem.Application.Command;
using TrainTicketBookingSystem.Application.Command.RegisterTrain;
using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Test.Application.RegisterTrain;

public class RegisterTrainCommandHandlerTest
{
    [Fact]
    public async Task Handle()
    {
        // Given
        var trainRepository = new Mock<ITrainRepository>();
        var mediator = new Mock<IMediator>();

        var command = new RegisterTrainCommand(100, [], Date.Create(DateTime.UtcNow));
        var handler = new RegisterTrainCommandHandler(trainRepository.Object, mediator.Object);

        // When
        var trainId = await handler.Handle(command, default);

        // Then
        trainRepository.Verify(x => x.AddAsync(It.Is<Train>(t => t.Id == trainId), default));
        mediator.Verify(x => x.PublishAsync(It.Is<DomainEvent>(e => e is TrainRegisteredDomainEvent), default));
    }
}