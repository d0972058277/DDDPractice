using System.Net.Http.Json;
using FluentAssertions;
using TrainTicketBookingSystem.WebApi;
using TrainTicketBookingSystem.WebApi.Controllers;

namespace TrainTicketBookingSystem.Test.WebApi;

public class TrainControllerTest
{
    public HttpClient _httpClient;
    public TrainTicketBoolingSystemDbContext _trainTicketBoolingSystemDbContext;

    public TrainControllerTest(HttpClient httpClient, TrainTicketBoolingSystemDbContext trainTicketBoolingSystemDbContext)
    {
        _httpClient = httpClient;
        _trainTicketBoolingSystemDbContext = trainTicketBoolingSystemDbContext;
    }

    [Fact]
    public async Task TestName()
    {
        // Given
        var registerTrainRequest = new RegisterTrainRequest { Seats = 10, Locations = ["taipei", "taichung"], Date = DateTime.UtcNow };
        var jsonContent = JsonContent.Create(registerTrainRequest);

        // When
        var httpResponse = await _httpClient.PostAsync("/api/train", jsonContent);
        var response = await httpResponse.Content.ReadFromJsonAsync<RegisterTrainResponse>();

        // Then
        var train = await _trainTicketBoolingSystemDbContext.Trains.FindAsync(response!.Id);
        train!.Seats.Should().Be(registerTrainRequest.Seats);
        train!.Locations.Should().BeEquivalentTo(registerTrainRequest.Locations.Select(location => Location.Create(location).Value));
        train!.Date.Should().Be(Date.Create(registerTrainRequest.Date).Value);
    }
}
