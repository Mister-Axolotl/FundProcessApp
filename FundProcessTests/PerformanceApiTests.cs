using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Globalization;
using FundProcess.API;

namespace FundProcess.Tests
{
    public class PerformanceApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PerformanceApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); // To send request without deploying API
        }

        [Fact]
        public async Task CalculatePerformance_WithValidDataset_ReturnsCorrectPerformance()
        {
            // Arrange
            var requestPayload = new PerformanceRequest
            {
                Dataset = new List<Tuple<DateTime, double>>
                {
                    Tuple.Create(new DateTime(2023, 1, 1), 100.0),
                    Tuple.Create(new DateTime(2023, 2, 1), 110.0)
                },
                FromDate = new DateTime(2023, 1, 1),
                ToDate = new DateTime(2023, 2, 1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/performance/calculate", requestPayload);
            response.EnsureSuccessStatusCode(); // Check if response is success

            var result = await response.Content.ReadFromJsonAsync<double>(); // Deserialize to double

            // Assert
            Assert.Equal(0.1, result, precision: 2);

        }
    }
}