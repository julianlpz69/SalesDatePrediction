using Xunit;
using Moq;
using API.Services;
using API.DTOs;
using API.Interfaces;
using System.Threading.Tasks;

public class EmployeeServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsEmployeeList()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(r => r.GetAllEmployeesAsync())
                .ReturnsAsync(new List<EmployeeDto>
                {
                    new EmployeeDto
                    {
                        Empid = 1,
                        Firstname = "John",
                        Lastname = "Doe",
                        Title = "Developer",
                        HireDate = new DateTime(2020, 1, 1),
                        City = "New York",
                        Country = "USA"
                    }
                });

        var service = new EmployeeService(mockRepo.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("John", result[0].Firstname);
        Assert.Equal("Doe", result[0].Lastname);
        Assert.Equal("Developer", result[0].Title);
        Assert.Equal("New York", result[0].City);
        Assert.Equal("USA", result[0].Country);
    }
}
