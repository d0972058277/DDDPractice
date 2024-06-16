using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection.AspNetCoreTesting;

namespace TrainTicketBookingSystem.Test;

public class Startup
{
    // public static void ConfigureServices(IServiceCollection _)
    // {
    // }

    public IHostBuilder CreateHostBuilder() => MinimalApiHostBuilderFactory.GetHostBuilder<Program>();
}
