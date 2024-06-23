using FluentAssertions;
using TrainTicketBookingSystem.Domain.Architecture;
using TrainTicketBookingSystem.Domain.Events;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Test.Domain;

public class TicketTest
{
    [Fact]
    public void 給予票務資料_應該預定一個未付款的Ticket()
    {
        // Given
        var id = Guid.NewGuid();
        var trainId = Guid.NewGuid();
        var from = Location.Create("taipei");
        var to = Location.Create("taichung");
        var date = Date.Create(DateTime.UtcNow);


        // When
        var ticket = Ticket.Book(id, trainId, from, to, date);

        // Then
        ticket.Id.Should().Be(id);
        ticket.TrainId.Should().Be(trainId);
        ticket.From.Should().Be(from);
        ticket.To.Should().Be(to);
        ticket.Date.Should().Be(date);
        ticket.PaymentStatus.Should().Be(PaymentStatus.Unpaid);
        ticket.DomainEvents.Should().Contain(new TicketBookedDomainEvent(id, trainId, from, to, date));
    }

    [Fact]
    public void 未付款_應該付款成功()
    {
        // Given
        var ticket = BookATicket();

        // When
        ticket.Pay();

        // Then
        ticket.PaymentStatus.Should().Be(PaymentStatus.Paid);
        ticket.DomainEvents.Should().Contain(new TicketPaidDomainEvent(ticket.Id));
    }

    [Fact]
    public void 已付款_應該付款失敗()
    {
        // Given
        var ticket = BookATicket();
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
            Location.Create("taipei"),
            Location.Create("taichung"),
            Date.Create(DateTime.UtcNow));
    }
}