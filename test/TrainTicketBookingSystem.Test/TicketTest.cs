using FluentAssertions;
using TrainTicketBookingSystem.Exceptions;

namespace TrainTicketBookingSystem.Test;

public class TicketTest
{
    [Fact]
    public void 給予票務資料_應該預定一個未付款的Ticket()
    {
        // Given
        var id = Guid.NewGuid();
        var trainId = Guid.NewGuid();
        var from = Location.Create("taipei").Value;
        var to = Location.Create("taichung").Value;
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

    [Fact]
    public void 未付款_應該付款成功()
    {
        // Given
        Ticket ticket = BookATicket();

        // When
        ticket.Pay();

        // Then
        ticket.PaymentStatus.Should().Be(PaymentStatus.Paid);
    }

    [Fact]
    public void 已付款_應該付款失敗()
    {
        // Given
        Ticket ticket = BookATicket();
        ticket.Pay();

        // When
        var action = () => ticket.Pay();

        // Then
        action.Should().Throw<DomainException>();
    }

    private static Ticket BookATicket()
    {
        return Ticket.Book(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Location.Create("taipei").Value,
            Location.Create("taichung").Value,
            Date.Create(DateTime.UtcNow).Value);
    }
}
