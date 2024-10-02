using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesApp.Data.Models;

namespace MoviesApp.Pages.Movies;

public class MoviesModel(ILogger<MoviesModel> logger) : PageModel
{
    private readonly ILogger<MoviesModel> _logger = logger;

    public List<Movie> Movies { get; set; } = [];

    /// <summary>
    /// Called when the page is accessed with an HTTP GET request.
    /// </summary>
    public void OnGet()
    {
        _logger.LogInformation("Fetching movies...");

        Movies = [
            new Movie { Id = 1, Title = "The Shawshank Redemption", Rating = 9, Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 2, Title = "The Godfather", Rating = 8, Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 3, Title = "The Dark Knight", Rating = 8, Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, the caped crusader must come to terms with one of the greatest psychological tests of his ability to fight injustice.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 4, Title = "12 Angry Men", Rating = 9, Description = "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 5, Title = "Schindler's List", Rating = 7, Description = "In German-occupied Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazi Germans.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 6, Title = "The Lord of the Rings: The Return of the King", Rating = 10, Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 7, Title = "Pulp Fiction", Rating = 6, Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 8, Title = "The Good, the Bad and the Ugly", Rating = 5, Description = "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 9, Title = "Forrest Gump", Rating = 8, Description = "The presidencies of Kennedy and Johnson, Vietnam, Watergate, and other historical events unfold through the perspective of an Alabama man with a low IQ.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
            new Movie { Id = 10, Title = "Inception", Rating = 7, Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", CreatedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
        ];
    }
}
