using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TrainTicketBookingSystem.Infrastructure;

public class DbContextFactory : IDesignTimeDbContextFactory<TrainTicketBookingSystemDbContext>
{
    public TrainTicketBookingSystemDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            "Server=localhost; Port=3306; User ID=root; Password=root; Database=TrainTicketBookingSystem;";
        var optionsBuilder = new DbContextOptionsBuilder<TrainTicketBookingSystemDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new TrainTicketBookingSystemDbContext(optionsBuilder.Options);
    }
}