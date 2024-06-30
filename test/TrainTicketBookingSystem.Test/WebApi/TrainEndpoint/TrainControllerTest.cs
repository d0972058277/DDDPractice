using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TrainTicketBookingSystem.Domain.Models;
using TrainTicketBookingSystem.WebApi;
using TrainTicketBookingSystem.WebApi.Controllers.TrainEndpoint;

namespace TrainTicketBookingSystem.Test.WebApi.TrainEndpoint;

[Collection("Sequential")]
public class TrainControllerTest
{
    private readonly HttpClient _httpClient;
    private readonly TrainTicketBookingSystemDbContext _trainTicketBookingSystemDbContext;

    public TrainControllerTest(HttpClient httpClient,
        TrainTicketBookingSystemDbContext trainTicketBookingSystemDbContext)
    {
        _httpClient = httpClient;
        _trainTicketBookingSystemDbContext = trainTicketBookingSystemDbContext;
        _trainTicketBookingSystemDbContext.Database.EnsureDeleted();
        _trainTicketBookingSystemDbContext.Database.Migrate();
    }

    [Fact]
    public async Task 應該成功登記一班火車()
    {
        // Given
        var request = new RegisterTrainRequest
            { Seats = 10, Locations = ["taipei", "taichung"], Date = DateTime.UtcNow };
        var jsonContent = JsonContent.Create(request);

        // When
        var httpResponse = await _httpClient.PostAsync("/api/train", jsonContent);
        var response = await httpResponse.Content.ReadFromJsonAsync<RegisterTrainResponse>();

        // Then
        var train = await _trainTicketBookingSystemDbContext.Trains.FindAsync(response!.Id);
        train!.Seats.Should().Be(request.Seats);
        train!.Locations.Should()
            .BeEquivalentTo(request.Locations.Select(Location.Create));
        train!.Date.Should().Be(Date.Create(request.Date));
    }
}