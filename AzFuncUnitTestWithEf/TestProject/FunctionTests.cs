using System.Text;
using DataContext.Data;
using DataContext.Entities;
using FunctionApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject;

public class FunctionTests
{
    private static readonly InMemoryDatabaseRoot _root = new();
    private BookDbContext _bookDbContext;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var dbBuilder = new DbContextOptionsBuilder<BookDbContext>();
        dbBuilder.UseInMemoryDatabase(databaseName: "Book", _root);
        _bookDbContext = new BookDbContext(dbBuilder.Options);
    }

    [SetUp]
    public async Task SetUp()
    {
        // Make sure database is empty when starting.
        await _bookDbContext.Database.EnsureDeletedAsync();
        await _bookDbContext.Database.EnsureCreatedAsync();
    }

    [Test]
    public async Task ReturnSuccess()
    {
        // Arrange
        var loggerFactory = new LoggerFactory();

        string input = "{\"Title\": \"Test Title\", \"Author\": \"Test Author\", \"PublishedDate\": \"2022-04-08T14:47:46Z\"}";
        
        using var requestStream = new MemoryStream();
        requestStream.Write(Encoding.ASCII.GetBytes(input));
        requestStream.Flush();
        requestStream.Position = 0;

        var requestMock = new Mock<HttpRequest>();
        requestMock.Setup(x => x.Body).Returns(requestStream);

        // Act
        var sut = new HttpTrigger(loggerFactory, _bookDbContext);
        var result = await sut.Run(requestMock.Object);

        var resultData = await _bookDbContext.Books.FirstOrDefaultAsync(x => x.Title == "Test Title");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.GetType(), Is.EqualTo(typeof(CreatedResult)));
            Assert.That(resultData, Is.Not.Null);
            Assert.That(resultData?.GetType(), Is.EqualTo(typeof(Book)));
        });
    }
}