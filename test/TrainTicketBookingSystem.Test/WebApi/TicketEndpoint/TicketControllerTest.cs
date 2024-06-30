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
    public async Task 給予訂票的所需資訊_應該成功訂一張票()
    {
        // Given
        var train = await RegisterATrainAsync();
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

    [Fact]
    public async Task 應該能夠成功付款()
    {
        // Given
        var train = await RegisterATrainAsync();
        var ticket = await BookATicketAsync(train);
        _trainTicketBookingSystemDbContext.ChangeTracker.Clear();

        // When
        var httpResponse = await _httpClient.PostAsync($"/api/ticket/{ticket.Id}/pay", new StringContent(string.Empty));

        // Then
        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        var actual = await _trainTicketBookingSystemDbContext.Tickets.FindAsync(ticket.Id);
        actual!.PaymentStatus.Should().Be(PaymentStatus.Paid);
    }

    private async Task<Ticket> BookATicketAsync(Train train)
    {
        var ticket = BookTrainTicketService.Execute(train, Guid.NewGuid(), Location.Create("taipei"),
            Location.Create("taichung"),
            Date.Create(DateTime.UtcNow));
        _trainTicketBookingSystemDbContext.Tickets.Add(ticket);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return ticket;
    }

    private async Task<Train> RegisterATrainAsync()
    {
        var train = Train.Register(Guid.NewGuid(), 10,
            new List<Location>() { Location.Create("taipei"), Location.Create("taichung") },
            Date.Create(DateTime.UtcNow));
        _trainTicketBookingSystemDbContext.Trains.Add(train);
        await _trainTicketBookingSystemDbContext.SaveChangesAsync();
        return train;
    }
}