using System.Text.Json;
using DataContext.Data;
using DataContext.Entities;
using DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp;

public class HttpTrigger(ILogger<HttpTrigger> logger, BookDbContext bookDbContext)
{
    private readonly ILogger<HttpTrigger> _logger = logger;
    private readonly BookDbContext _bookDbContext = bookDbContext;

    [Function("HttpTrigger")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("HTTP trigger function processed a request.");

            var book = await ParseInput(req);

            _bookDbContext.Books.Add(Map(book));
            await _bookDbContext.SaveChangesAsync();

            return new CreatedResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ContentResult()
            {
                Content = ex.Message,
                ContentType = "application/json",
                StatusCode = 500
            };
        }
    }

    private async Task<BookInput> ParseInput(HttpRequest req)
    {
        string requestBody = string.Empty;

        using StreamReader streamReader = new(req.Body);
        requestBody = await streamReader.ReadToEndAsync();

        return JsonSerializer.Deserialize<BookInput>(requestBody) ?? new BookInput();
    }

    private Book Map(BookInput b)
    {
        return new Book { Title = b.Title, Author = b.Author, PublishDate = b.PublishedDate };
    }
}
