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

    public Movie GetById(int id)
    {
        var movieDetails = _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(mc => mc.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefault(m => m.Id == id);
        return movieDetails;
    }

    public List<Movie> GetTop30GrossingMovies()
    {
        var movies = _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
        return movies;
    }
}