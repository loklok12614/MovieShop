using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    
    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    public List<MovieCardModel> GetTop30GrossingMovies()
    {
        var movies = _movieRepository.GetTop30GrossingMovies();

        var movieCards = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            movieCards.Add(new MovieCardModel{Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl});
        }

        return movieCards;
    }

    public MovieDetailsModel GetMovieDetails(int movieId)
    {
        var movieDetails = _movieRepository.GetById(movieId);

        var movieDetailsModel = new MovieDetailsModel
        {
            Id = movieDetails.Id,
            Title = movieDetails.Title,
            Overview = movieDetails.Overview,
            Tagline = movieDetails.Tagline,
            Budget = movieDetails.Budget,
            Revenue = movieDetails.Revenue,
            ImdbUrl = movieDetails.ImdbUrl,
            TmdbUrl = movieDetails.TmdbUrl,
            PosterUrl = movieDetails.PosterUrl,
            BackdropUrl = movieDetails.BackdropUrl,
            OriginalLanguage = movieDetails.OriginalLanguage,
            ReleaseDate = movieDetails.ReleaseDate,
            RunTime = movieDetails.RunTime,
            Price = movieDetails.Price
        };

        foreach (var trailer in movieDetails.Trailers)
        {
            movieDetailsModel.Trailers.Add(new TrailerModel
            {
                Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl
            });
        }
        
        foreach (var cast in movieDetails.CastsOfMovie)
        {
            movieDetailsModel.Casts.Add(new CastModel
            {
                Id = cast.CastId, Name = cast.Cast.Name, ProfilePath = cast.Cast.ProfilePath, TmdbUrl = cast.Cast.TmdbUrl, Character = cast.Character
            });
        }
        
        foreach (var genre in movieDetails.GenresOfMovie)
        {
            movieDetailsModel.Genres.Add(new GenreModel
            {
                Id = genre.GenreId, Name = genre.Genre.Name
            });
        }

        return movieDetailsModel;
    }
}