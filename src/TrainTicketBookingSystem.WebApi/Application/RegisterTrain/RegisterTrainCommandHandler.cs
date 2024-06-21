namespace TrainTicketBookingSystem.WebApi.Application.RegisterTrain;

public class RegisterTrainCommandHandler
{
    private readonly TrainTicketBookingSystemDbContext _trainTicketBookingSystemDbContext;

    public RegisterTrainCommandHandler(TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _trainTicketBookingSystemDbContext = trainTicketBookingSystemDbContext;
    }

    public async Task<Guid> Handle(RegisterTrainCommand registerTrainCommand)
    {
        var id = Guid.NewGuid();
        var train = Train.Register(id, registerTrainCommand.Seats,
            registerTrainCommand.Locations.Select(x => Location.Create(x).Value),
            Date.Create(registerTrainCommand.DateTime).Value);
        _trainTicketBookingSystemDbContext.Add(train);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return id;
    }
}