using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection.AspNetCoreTesting;

namespace TrainTicketBookingSystem.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder.UseEnvironment("Development");
    }

    public IHostBuilder CreateHostBuilder() => MinimalApiHostBuilderFactory.GetHostBuilder<Program>();
}
