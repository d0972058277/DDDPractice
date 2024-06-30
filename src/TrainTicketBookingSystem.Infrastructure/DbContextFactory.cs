using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TrainTicketBookingSystem.Infrastructure;

public class DbContextFactory : IDesignTimeDbContextFactory<TrainTicketBookingSystemDbContext>
{
    public TrainTicketBookingSystemDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TrainTicketBookingSystemDbContext>();
        var connectionString =
            "Server=localhost; Port=3306; User ID=root; Password=root; Database=TrainTicketBookingSystem;";
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new TrainTicketBookingSystemDbContext(builder.Options);
    }

    // public TrainTicketBookingSystemDbContext CreateDbContext(string[] args)
    // {
    //     var builder = new DbContextOptionsBuilder<TrainTicketBookingSystemDbContext>();
    //     builder.UseMongoDB("mongodb://localhost:27017", "TrainTicketBookingSystem");
    //     return new TrainTicketBookingSystemDbContext(builder.Options);
    // }
}