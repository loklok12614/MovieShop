using System.Data;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Dapper;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;
    private readonly IDbConnection connection;

    public MovieRepository(MovieShopDbContext movieShopDbContext, IConfiguration configuration)
    {
        _movieShopDbContext = movieShopDbContext;
        connection = new SqlConnection(configuration.GetConnectionString("MovieShopDbConnection"));
    }

    public async Task<Movie> GetById(int id)
    {
        var movieDetails = await _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(mg => mg.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(mc => mc.Cast)
            .Include(m=> m.UsersReviewed)
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

    public async Task<PagedResultSet<Movie>> GetAllMoviesDapper(int pageSize = 30, int page = 1)
    {
        var totalMoviesCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(Id) FROM Movies");
        if (totalMoviesCount == 0)
        {
            throw new Exception("No Movie Found For This Genre");
        }

        var query = "SELECT Id, PosterUrl, Title " +
                    "FROM Movies " +
                    "ORDER BY Revenue DESC " +
                    "OFFSET @Offset ROWS " +
                    "FETCH NEXT @Fetch ROWS ONLY";
            
        var parameters = new DynamicParameters();
        parameters.Add("@Offset", (page - 1) * pageSize);
        parameters.Add("@Fetch", pageSize);
            
        var movies = await connection.QueryAsync<Movie>(query, parameters);
        var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCount);
        return pagedMovies;
    }

    public async Task<List<Movie>> GetTop30GrossingMovies()
    {
        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }
    
    public async Task<List<Movie>> GetTop30RatedMovies()
    {
        var movies = await _movieShopDbContext.Movies
            .Where(m=>m.UsersReviewed.Count > 0)
            .OrderByDescending(m => m.UsersReviewed.Average(r=>r.Rating))
            .Take(30)
            .ToListAsync();
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

    public async Task<Movie> CreateMovie(Movie movie)
    {
        _movieShopDbContext.Movies.Add(movie);
        await _movieShopDbContext.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> EditMovie(Movie movie)
    {
        _movieShopDbContext.Entry(movie).State = EntityState.Modified;
        await _movieShopDbContext.SaveChangesAsync();
        return movie;
    }
}