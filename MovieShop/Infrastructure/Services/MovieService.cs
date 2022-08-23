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
}