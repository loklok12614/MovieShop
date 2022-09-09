namespace ApplicationCore.Models;

public class CreateMovieRequestModel
{
    public CreateMovieRequestModel()
    {
        Genres = new List<GenreModel>();
    }
    
    public string Title { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string Tagline { get; set; } = null!;
    public decimal? Budget { get; set; }
    public decimal? Revenue { get; set; }
    public string ImdbUrl { get; set; } = null!;
    public string TmdbUrl { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string BackdropUrl { get; set; } = null!;
    public string OriginalLanguage { get; set; } = null!;
    
    public List<GenreModel> Genres { get; set; }
}