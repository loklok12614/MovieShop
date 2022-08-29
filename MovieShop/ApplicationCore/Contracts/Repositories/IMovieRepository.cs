using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    //CRUD methods
    Task<Movie> GetById(int id);
    
    //get top 30 grossing movies
    Task<List<Movie>> GetTop30GrossingMovies();
}