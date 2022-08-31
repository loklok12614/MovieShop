using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    //CRUD methods
    Task<Movie> GetById(int id);
    
    //get all movies
    Task< Tuple< List<Movie>, int>> GetAllMovies(int pageNumber, int pageSize);
    
    //get top 30 grossing movies
    Task<List<Movie>> GetTop30GrossingMovies();
}