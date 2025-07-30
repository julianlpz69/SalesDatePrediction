using Xunit;
using API.Services;
using API.Models;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Repositories;
using System.Threading.Tasks;
using System;

public class OrderServiceTests
{
    private StoreSampleContext GetRealDbContext()
    {
        var options = new DbContextOptionsBuilder<StoreSampleContext>()
            .UseSqlServer("Server=localhost;Database=StoreSample;User Id=sa;Password=Julianlpz69;TrustServerCertificate=True;")
            .Options;

        return new StoreSampleContext(options);
    }

    [Fact]
    public async Task CreateOrderAsync_ValidData_ReturnsOrderId()
    {
        // Arrange
        var context = GetRealDbContext();
        var repository = new OrderRepository(context);
        var service = new OrderService(context, repository);

        var dto = new CreateOrderDto
        {
            CustomerId = 1, 
            EmployeeId = 1, 
            ShipperId = 1,  
            ProductId = 1, 
            Quantity = 2,
            UnitPrice = 15.5m,
            Discount = 0,
            ShipName = "Test Ship",
            ShipAddress = "123 Street",
            ShipCity = "Test City",
            ShipCountry = "Test Country",
            Freight = 10
        };

        // Act
        var result = await service.CreateOrderAsync(dto);

        // Assert
        Assert.True(result > 0); // It should return an ID greater than 0 if it was created successfully.
    }
}
