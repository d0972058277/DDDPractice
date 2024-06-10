using FluentAssertions;

namespace TrainTicketBookingSystem.Test;

public class TicketTest
{
    [Fact]
    public void 給予票務資料_應該預定一個未付款的Ticket()
    {
        // Given
        var id = Guid.NewGuid();
        var trainId = Guid.NewGuid();
        var from = Location.Create("台北").Value;
        var to = Location.Create("台中").Value;
        var date = Date.Create(DateTime.UtcNow).Value;


        // When
        var ticket = Ticket.Book(id, trainId, from, to, date);

        // Then
        ticket.Id.Should().Be(id);
        ticket.TrainId.Should().Be(trainId);
        ticket.From.Should().Be(from);
        ticket.To.Should().Be(to);
        ticket.Date.Should().Be(date);
        ticket.PaymentStatus.Should().Be(PaymentStatus.Unpaid);
    }
}
