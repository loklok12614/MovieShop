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

    public async Task<PagedResultSet<MovieCardModel>> GetAllMovies(int pageSize = 30, int page = 1)
    {
        var movies = await _movieRepository.GetAllMovies(pageSize, page);

        var movieCards = new List<MovieCardModel>();
        movieCards.AddRange(movies.Data.Select(m => new MovieCardModel
        {
            Id = m.Id,
            PosterUrl = m.PosterUrl,
            Title = m.Title
        }));

        return new PagedResultSet<MovieCardModel>(movieCards, page, pageSize, movies.TotalRowCount);
    }

    public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
    {
        var movies = await _movieRepository.GetTop30GrossingMovies();

        var movieCards = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            movieCards.Add(new MovieCardModel{Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl});
        }

        return movieCards;
    }

    public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
    {
        var movieDetails = await _movieRepository.GetById(movieId);

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
            Price = movieDetails.Price,
            Rating = movieDetails.Rating
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

    public async Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1)
    {
        var movies = await _movieRepository.GetMoviesByGenrePagination(genreId, pageSize, page);

        var movieCards = new List<MovieCardModel>();
        
        movieCards.AddRange( movies.Data.Select(m=>new MovieCardModel
        {
            Id = m.Id,
            PosterUrl = m.PosterUrl,
            Title = m.Title
        }));

        return new PagedResultSet<MovieCardModel>(movieCards, page, pageSize, movies.TotalRowCount);
    }
}