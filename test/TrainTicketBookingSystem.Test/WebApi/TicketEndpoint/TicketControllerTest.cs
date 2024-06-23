using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TrainTicketBookingSystem.Domain.Models;
using TrainTicketBookingSystem.WebApi;
using TrainTicketBookingSystem.WebApi.Controllers.TicketEndpoint;

namespace TrainTicketBookingSystem.Test.WebApi.TicketEndpoint;

[Collection("Sequential")]
public class TicketControllerTest
{
    private readonly HttpClient _httpClient;
    private readonly TrainTicketBookingSystemDbContext _trainTicketBookingSystemDbContext;

    public TicketControllerTest(HttpClient httpClient,
        TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _httpClient = httpClient;
        _trainTicketBookingSystemDbContext = trainTicketBookingSystemDbContext;
        _trainTicketBookingSystemDbContext.Database.EnsureDeleted();
        _trainTicketBookingSystemDbContext.Database.Migrate();
    }

    [Fact]
    public async Task TestName()
    {
        // Given
        var train = Train.Register(Guid.NewGuid(), 10,
            new List<Location>() { Location.Create("taipei"), Location.Create("taichung") },
            Date.Create(DateTime.UtcNow));
        _trainTicketBookingSystemDbContext.Trains.Add(train);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        _trainTicketBookingSystemDbContext.ChangeTracker.Clear();

        var request = new BookTicketRequest()
            { TrainId = train.Id, From = "taipei", To = "taichung", Date = DateTime.UtcNow };
        var jsonContent = JsonContent.Create(request);

        // When
        var httpResponse = await _httpClient.PostAsync("/api/ticket", jsonContent);
        var response = await httpResponse.Content.ReadFromJsonAsync<BookTicketResponse>();

        // Then
        var ticket = await _trainTicketBookingSystemDbContext.Tickets.FindAsync(response!.Id);
        ticket!.TrainId.Should().Be(request.TrainId);
        ticket.From.Should().Be(Location.Create(request.From));
        ticket.To.Should().Be(Location.Create(request.To));
        ticket.Date.Should().Be(Date.Create(request.Date));
    }
}