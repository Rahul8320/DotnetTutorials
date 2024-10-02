using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesApp.Data.Models;

namespace MoviesApp.Pages.Movies;

/// <summary>
/// Page for creating a new movie.
/// </summary>
public class CreateMovieModel(ILogger<CreateMovieModel> logger) : PageModel
{
    private readonly ILogger<CreateMovieModel> _logger = logger;

    [BindProperty]
    public string Title { get; set; } = string.Empty;
    [BindProperty]
    public int Rating { get; set; }
    [BindProperty]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Called when the page is accessed with an HTTP GET request.
    /// </summary>
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var newMovie = new Movie()
        {
            Title = Title,
            Description = Description,
            Rating = Rating,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow
        };

        return Page();
    }
}
