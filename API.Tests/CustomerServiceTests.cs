using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Services;

public class CustomerServiceTests
{
    [Fact]
    public async Task GetPredictionsAsync_ReturnsPagedPredictions()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();

        var sampleData = new List<CustomerPredictionDto>
        {
            new CustomerPredictionDto
            {
                CustomerName = "Acme Corp",
                LastOrderDate = new DateTime(2024, 1, 30),
                NextPredictedOrder = new DateTime(2024, 2, 15)
            },
            new CustomerPredictionDto
            {
                CustomerName = "Globex Inc",
                LastOrderDate = new DateTime(2024, 2, 5),
                NextPredictedOrder = new DateTime(2024, 2, 28)
            }
        };

        int expectedTotal = sampleData.Count;
        int page = 1;
        int pageSize = 10;
        string sortBy = "CustomerName";
        string sortOrder = "asc";
        string? search = null;

        mockRepo.Setup(r => r.GetPredictionsAsync(page, pageSize, sortBy, sortOrder, search))
                .ReturnsAsync((sampleData, expectedTotal));

        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.GetPredictionsAsync(page, pageSize, sortBy, sortOrder, search);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTotal, result.Total);
        Assert.Equal(sampleData.Count, result.Data.Count);
        Assert.Equal("Acme Corp", result.Data[0].CustomerName);
        Assert.True(result.Data[0].NextPredictedOrder > result.Data[0].LastOrderDate);
    }
}
