using Xunit;
using Moq;
using API.Services;
using API.Interfaces;
using API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ShipperServiceTests
{
    [Fact]
    public async Task GetAllShippersAsync_ReturnsShipperList()
    {
        // Arrange
        var mockRepo = new Mock<IShipperRepository>();
        mockRepo.Setup(repo => repo.GetAllShippersAsync())
            .ReturnsAsync(new List<ShipperDto>
            {
                new ShipperDto
                {
                    ShipperId = 1,
                    CompanyName = "Fast Delivery Co.",
                    Phone = "123-456-7890"
                }
            });

        var service = new ShipperService(mockRepo.Object);

        // Act
        var result = await service.GetAllShippersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        var shipper = result[0];
        Assert.Equal(1, shipper.ShipperId);
        Assert.Equal("Fast Delivery Co.", shipper.CompanyName);
        Assert.Equal("123-456-7890", shipper.Phone);
    }
}
