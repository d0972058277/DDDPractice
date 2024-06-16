using FluentAssertions;

namespace TrainTicketBookingSystem.Test.Domain;

public class LocationTest
{
    [Fact]
    public void 給予名稱_應該正確建立Location()
    {
        // Given
        var name = "台中";

        // When
        var result = Location.Create(name);

        // Then
        result.Value.Name.Should().Be(name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void 應該不可以建立名稱為空的Location(string name)
    {
        // Given

        // When
        var result = Location.Create(name);

        // Then
        result.IsFailure.Should().BeTrue();
    }
}
