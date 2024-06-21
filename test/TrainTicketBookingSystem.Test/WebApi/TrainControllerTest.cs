using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TrainTicketBookingSystem.WebApi;
using TrainTicketBookingSystem.WebApi.Controllers;

namespace TrainTicketBookingSystem.Test.WebApi;

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
    public async Task TestName()
    {
        // Given
        var registerTrainRequest = new RegisterTrainRequest
            { Seats = 10, Locations = ["taipei", "taichung"], Date = DateTime.UtcNow };
        var jsonContent = JsonContent.Create(registerTrainRequest);

        // When
        var httpResponse = await _httpClient.PostAsync("/api/train", jsonContent);
        var response = await httpResponse.Content.ReadFromJsonAsync<RegisterTrainResponse>();

        // Then
        var train = await _trainTicketBookingSystemDbContext.Trains.FindAsync(response!.Id);
        train!.Seats.Should().Be(registerTrainRequest.Seats);
        train!.Locations.Should()
            .BeEquivalentTo(registerTrainRequest.Locations.Select(location => Location.Create(location).Value));
        train!.Date.Should().Be(Date.Create(registerTrainRequest.Date).Value);
    }
}