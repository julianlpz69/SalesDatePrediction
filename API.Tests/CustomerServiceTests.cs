using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Services;
using API.Models;

public class CustomerServiceTests
{
    [Fact]
    public async Task GetPredictionsAsync_ReturnsPredictedOrders()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        var sampleOrders = new List<Order>
        {
            new Order { Custid = 1, Orderdate = new DateTime(2024, 1, 1), Cust = new Customer { Companyname = "Acme Corp" } },
            new Order { Custid = 1, Orderdate = new DateTime(2024, 1, 15), Cust = new Customer { Companyname = "Acme Corp" } },
            new Order { Custid = 1, Orderdate = new DateTime(2024, 1, 30), Cust = new Customer { Companyname = "Acme Corp" } }
        };

        mockRepo.Setup(r => r.GetAllOrdersWithCustomerAsync())
                .ReturnsAsync(sampleOrders);

        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.GetPredictionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        var prediction = result[0];
        Assert.Equal("Acme Corp", prediction.CustomerName);
        Assert.Equal(new DateTime(2024, 1, 30), prediction.LastOrderDate);
        Assert.True(prediction.NextPredictedOrder > prediction.LastOrderDate); // It must be an estimated future date.
    }
}
