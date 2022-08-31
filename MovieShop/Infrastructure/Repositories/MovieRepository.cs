using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
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

    public async Task< Tuple< List<Movie>, int>> GetAllMovies(int pageNumber, int pageSize)
    {
        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).ToListAsync();
        var paginatedMovies = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        int totalMovies = movies.Count();
        return Tuple.Create(paginatedMovies, totalMovies);
    }

    public async Task<List<Movie>> GetTop30GrossingMovies()
    {
        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }
}