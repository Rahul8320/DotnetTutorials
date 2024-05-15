using FluentAssertions;
using FormulaApp.Api.Controllers;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services.Interfaces;
using FormulaApp.UnitTest.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FormulaApp.UnitTest.Systems.Controllers;

public class TestFansController
{
    [Fact]
    public async Task GetAll_OnSuccess_ReturnStatusCode200()
    {
        // Arrange
        var mockFanService = new Mock<IFanService>();
        mockFanService
            .Setup(service => service.GetAllFans())
            .ReturnsAsync(FanFixtures.GetFans());

        var fansController = new FansController(mockFanService.Object);

        // Act
        var result = (OkObjectResult)await fansController.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetAll_OnSuccess_InvokeService()
    {
        // Arrange
        var mockFanService = new Mock<IFanService>();
        mockFanService
            .Setup(service => service.GetAllFans())
            .ReturnsAsync(FanFixtures.GetFans());

        var fansController = new FansController(mockFanService.Object);

        // Act
        var result = (OkObjectResult)await fansController.GetAll();

        // Assert
        mockFanService.Verify(service => service.GetAllFans(), Times.Once());
    }

    [Fact]
    public async Task GetAll_OnSuccess_ReturnListOfFans()
    {
        // Arrange
        var mockFanService = new Mock<IFanService>();
        mockFanService
            .Setup(service => service.GetAllFans())
            .ReturnsAsync(FanFixtures.GetFans());

        var fansController = new FansController(mockFanService.Object);

        // Act
        var result = (OkObjectResult)await fansController.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeOfType<List<Fan>>();
    }

    [Fact]
    public async Task GetAll_OnNoFans_ReturnStatusCode404()
    {
        // Arrange
        var mockFanService = new Mock<IFanService>();
        mockFanService
            .Setup(service => service.GetAllFans())
            .ReturnsAsync([]);

        var fansController = new FansController(mockFanService.Object);

        // Act
        var result = (NotFoundResult)await fansController.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
        result.StatusCode.Should().Be(404);
    }
}
