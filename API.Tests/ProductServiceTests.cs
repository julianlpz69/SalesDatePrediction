using Xunit;
using Moq;
using API.Services;
using API.DTOs;
using API.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllProductsAsync_ReturnsProductList()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(repo => repo.GetAllProductsAsync())
            .ReturnsAsync(new List<ProductDto>
            {
                new ProductDto
                {
                    ProductId = 1,
                    ProductName = "Laptop",
                    UnitPrice = 1500.00m,
                    Discontinued = false,
                    CategoryName = "Electronics",
                    SupplierName = "TechSupplier Inc."
                }
            });

        var service = new ProductService(mockRepo.Object);

        // Act
        var result = await service.GetAllProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        var product = result[0];
        Assert.Equal(1, product.ProductId);
        Assert.Equal("Laptop", product.ProductName);
        Assert.Equal(1500.00m, product.UnitPrice);
        Assert.False(product.Discontinued);
        Assert.Equal("Electronics", product.CategoryName);
        Assert.Equal("TechSupplier Inc.", product.SupplierName);
    }
}
