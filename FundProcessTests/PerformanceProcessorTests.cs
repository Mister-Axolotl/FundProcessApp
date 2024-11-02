using System;
using Xunit;
using FundProcessLibrary;

namespace FundProcess.Tests
{
    public class PerformanceProcessorTests
    {
        private readonly IPerformanceProcessor _processor;


        public PerformanceProcessorTests()
        {
            _processor = new PerformanceProcessor();
        }

        [Fact]
        public void GetPerformance_WithValidDataset_ReturnsCorrectPerformance()
        {
            // Arrange
            var dataset = new[]
            {
                Tuple.Create(new DateTime(2023, 1, 1), 100.0),
                Tuple.Create(new DateTime(2023, 2, 1), 110.0)
            };
            var fromDate = new DateTime(2023, 1, 1);
            var toDate = new DateTime(2023, 2, 1);

            // Act
            var result = _processor.GetPerformance(dataset, fromDate, toDate);

            // Assert
            Assert.Equal(0.1, result, precision: 2); // 10% de performance
        }

        [Fact]
        public void GetPerformance_WithEmptyDataset_ReturnsZero()
        {
            // Arrange
            var dataset = Array.Empty<Tuple<DateTime, double>>();
            var fromDate = new DateTime(2023, 1, 1);
            var toDate = new DateTime(2023, 2, 1);

            // Act
            var result = _processor.GetPerformance(dataset, fromDate, toDate);

            // Assert
            Assert.Equal(0, result); // No data
        }

        [Fact]
        public void GetPerformance_WithSingleDataPoint_ReturnsZero()
        {
            // Arrange
            var dataset = new[]
            {
                Tuple.Create(new DateTime(2023, 1, 1), 100.0)
            };
            var fromDate = new DateTime(2023, 1, 1);
            var toDate = new DateTime(2023, 2, 1);

            // Act
            var result = _processor.GetPerformance(dataset, fromDate, toDate);

            // Assert
            Assert.Equal(0, result); // Not enough points
        }

        [Fact]
        public void GetPerformance_WithNoDataInRange_ReturnsZero()
        {
            // Arrange
            var dataset = new[]
            {
                Tuple.Create(new DateTime(2022, 1, 1), 100.0),
                Tuple.Create(new DateTime(2022, 2, 1), 110.0)
            };
            var fromDate = new DateTime(2023, 1, 1);
            var toDate = new DateTime(2023, 2, 1);

            // Act
            var result = _processor.GetPerformance(dataset, fromDate, toDate);

            // Assert
            Assert.Equal(0, result); // No points in the date range
        }

        [Fact]
        public void GetPerformance_WithZeroAsFirstValue_ReturnsZero()
        {
            // Arrange
            var dataset = new[]
            {
                Tuple.Create(new DateTime(2023, 1, 1), 0.0),
                Tuple.Create(new DateTime(2023, 2, 1), 110.0)
            };
            var fromDate = new DateTime(2023, 1, 1);
            var toDate = new DateTime(2023, 2, 1);

            // Act
            var result = _processor.GetPerformance(dataset, fromDate, toDate);

            // Assert
            Assert.Equal(0, result); // Division by zero avoided
        }
    }
}
