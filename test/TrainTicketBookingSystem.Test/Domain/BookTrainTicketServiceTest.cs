using FluentAssertions;
using TrainTicketBookingSystem.Exceptions;

namespace TrainTicketBookingSystem.Test.Domain;

public class BookTrainTicketServiceTest
{
    [Fact]
    public void 火車未滿_應該訂票成功()
    {
        // Given
        var taipei = Location.Create("taipei").Value;
        var taichung = Location.Create("taichung").Value;
        var date = Date.Create(DateTime.UtcNow).Value;

        var trainId = Guid.NewGuid();
        var train = Train.Register(trainId, 20, new List<Location>() { taipei, taichung }, date);

        // When
        var ticketId = Guid.NewGuid();
        var ticket = BookTrainTicketService.Execute(train, ticketId, taipei, taichung, date);

        // Then
        ticket.Id.Should().Be(ticketId);
        ticket.TrainId.Should().Be(trainId);
        ticket.From.Should().Be(taipei);
        ticket.To.Should().Be(taichung);
        ticket.Date.Should().Be(date);
        ticket.PaymentStatus.Should().Be(PaymentStatus.Unpaid);
    }

    [Fact]
    public void 火車已滿_應該訂票失敗()
    {
        // Given
        var taipei = Location.Create("taipei").Value;
        var taichung = Location.Create("taichung").Value;
        var date = Date.Create(DateTime.UtcNow).Value;

        var trainId = Guid.NewGuid();
        var train = Train.Register(trainId, 0, new List<Location>() { taipei, taichung }, date);

        // When
        var ticketId = Guid.NewGuid();
        var action = () => BookTrainTicketService.Execute(train, ticketId, taipei, taichung, date);

        // Then
        action.Should().Throw<DomainException>();
    }
}
