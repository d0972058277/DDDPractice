namespace TrainTicketBookingSystem.WebApi.Application.RegisterTrain;

public class RegisterTrainCommandHandler
{
    private readonly ITrainRepository _trainRepository;

    public RegisterTrainCommandHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    public async Task<Guid> HandleAsync(RegisterTrainCommand registerTrainCommand)
    {
        var id = Guid.NewGuid();
        var train = Train.Register(id, registerTrainCommand.Seats,
            registerTrainCommand.Locations.Select(x => Location.Create(x).Value),
            Date.Create(registerTrainCommand.DateTime).Value);
        await _trainRepository.AddAsync(train);
        return id;
    }
}