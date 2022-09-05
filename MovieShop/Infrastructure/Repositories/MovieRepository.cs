using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public MovieRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<Movie> GetById(int id)
    {
        var movieDetails = await _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(mc => mc.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefaultAsync(m => m.Id == id);
        return movieDetails;
    }

    public async Task<PagedResultSet<Movie>> GetAllMovies(int pageSize = 30, int page = 1)
    {
        var totalMoviesCount = await _movieShopDbContext.Movies.CountAsync();
        if (totalMoviesCount == 0)
        {
            throw new Exception("No Movie Found For This Genre");
        }

        var movies = await _movieShopDbContext.Movies
            .OrderByDescending(m => m.Revenue)
            .Select(m => new Movie
            {
                Id = m.Id,
                PosterUrl = m.PosterUrl,
                Title = m.Title
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCount);
        return pagedMovies;
    }

    public async Task<List<Movie>> GetTop30GrossingMovies()
    {
        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }

    public async Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1)
    {
        // Get total row Count
        var totalMoviesCountOfGenre = await _movieShopDbContext.MovieGenres.Where(mg => mg.GenreId == genreId).CountAsync();
        if (totalMoviesCountOfGenre == 0)
        {
            throw new Exception("No Movie Found For This Genre");
        }

        var movies = await _movieShopDbContext.MovieGenres.Where(mg => mg.GenreId == genreId)
            .Include(mg => mg.Movie)
            .OrderByDescending(m => m.Movie.Revenue)
            .Select(mg=> new Movie
            {
                Id = mg.MovieId,
                PosterUrl = mg.Movie.PosterUrl,
                Title = mg.Movie.Title
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCountOfGenre);
        return pagedMovies;
    }
}