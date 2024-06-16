using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTicketBookingSystem.Test.WebApi;

public class TrainControllerTest
{
    public HttpClient _httpClient;

    public TrainControllerTest(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Fact]
    public async Task TestName()
    {
        // Given


        // When
        var response = await _httpClient.PostAsync("/api/train", null);

        // Then
        response.EnsureSuccessStatusCode();
    }
}
