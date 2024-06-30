using Amazon.Runtime.Internal;
using FluentAssertions;
using Moq;
using TrainTicketBookingSystem.Application.Command.RegisterTrain;
using TrainTicketBookingSystem.Domain.Models;
using TrainTicketBookingSystem.Infrastructure.Architecture;
using IMediator = MediatR.IMediator;

namespace TrainTicketBookingSystem.Test.Infrastructure.Architecture;

public class MediatorTest
{
    private readonly IMediator _mediator;

    public MediatorTest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Fact]
    public async Task Send()
    {
        // Given
        var mediator = new Mediator(_mediator);
        var command = new TestCommand();

        // When
        var result = await mediator.ExecuteAsync(command);

        // Then
        result.Should().BeTrue();
    }
}