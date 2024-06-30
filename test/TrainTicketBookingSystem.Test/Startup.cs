using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TrainTicketBookingSystem.Application;
using TrainTicketBookingSystem.Infrastructure;
using Xunit.DependencyInjection.AspNetCoreTesting;

namespace TrainTicketBookingSystem.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder.UseEnvironment("Development");
        hostBuilder.ConfigureServices(serivce =>
        {
            serivce.TryAddScoped<ITrainRepository, TrainRepository>();
            serivce.TryAddScoped<ITicketRepository, TicketRepository>();

            serivce.TryAddScoped<TrainTicketBookingSystemDbContext>(provider =>
            {
                var builder = new DbContextOptionsBuilder<TrainTicketBookingSystemDbContext>();
                const string connectionString =
                    "Server=localhost; Port=3306; User ID=root; Password=root; Database=TrainTicketBookingSystem;";
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                return new TrainTicketBookingSystemDbContext(builder.Options);
            });

            // serivce.TryAddScoped<TrainTicketBookingSystemDbContext>(provider =>
            // {
            //     var builder = new DbContextOptionsBuilder<TrainTicketBookingSystemDbContext>();
            //     builder.UseMongoDB("mongodb://localhost:27017", "TrainTicketBookingSystem");
            //     return new TrainTicketBookingSystemDbContext(builder.Options);
            // });
        });
    }

    public IHostBuilder CreateHostBuilder()
    {
        return MinimalApiHostBuilderFactory.GetHostBuilder<Program>();
    }
}