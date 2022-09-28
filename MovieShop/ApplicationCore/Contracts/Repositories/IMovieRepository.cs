using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    //CRUD methods
    Task<Movie> GetById(int id);
    
    //get all movies
    Task<PagedResultSet<Movie>> GetAllMovies(int pageSize = 30, int page = 1);
    Task<PagedResultSet<Movie>> GetAllMoviesDapper(int pageSize = 30, int page = 1);

    //get top 30 grossing movies
    Task<List<Movie>> GetTop30GrossingMovies();
    Task<List<Movie>> GetTop30RatedMovies();
    Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1);
    
    // Admin
    Task<Movie> CreateMovie(Movie movie);
    Task<Movie> EditMovie(Movie movie);
}