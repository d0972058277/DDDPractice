using TrainTicketBookingSystem.Application.Architecture;

namespace TrainTicketBookingSystem.Test.Infrastructure.Architecture;

public class TestCommandHandler : ICommandHandler<TestCommand, bool>
{
    public Task<bool> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}