using FluentAssertions;
using FormulaApp.Api.Configurations;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services;
using FormulaApp.UnitTest.Fixtures;
using FormulaApp.UnitTest.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace FormulaApp.UnitTest.Systems.Services;

public class TestFanService
{
    private readonly string _url = "https://mywebsite.com/api/v1/fans";

    [Fact]
    public async Task GetAllFans_OnInvoked_HttpGet()
    {
        // Arrange
        var response = FanFixtures.GetFans();
        var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(
            httpClient: httpClient,
            config: config);

        // Act
        await fanService.GetAllFans();

        // Assert
        mockHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri!.ToString() == _url),
                ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetAllFans_OnInvoked_ListOfFans()
    {
        // Arrange
        var response = FanFixtures.GetFans();
        var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(
            httpClient: httpClient,
            config: config);

        // Act
        var result = await fanService.GetAllFans();

        // Assert
        result.Should().BeOfType<List<Fan>>();
    }

    [Fact]
    public async Task GetAllFans_OnInvoked_ReturnEmptyList()
    {
        // Arrange
        var mockHandler = MockHttpHandler<Fan>.SetupReturnNotFound();
        var httpClient = new HttpClient(mockHandler.Object);

        var config = Options.Create(new ApiServiceConfig()
        {
            Url = _url
        });
        var fanService = new FanService(
            httpClient: httpClient,
            config: config);

        // Act
        var result = await fanService.GetAllFans();

        // Assert
        result?.Count.Should().Be(0);
    }
}
