using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    //CRUD methods
    Task<Movie> GetById(int id);
    
    //get all movies
    Task<PagedResultSet<Movie>> GetAllMovies(int pageSize = 30, int page = 1);
    
    //get top 30 grossing movies
    Task<List<Movie>> GetTop30GrossingMovies();
    Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1);
}