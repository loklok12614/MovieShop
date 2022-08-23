using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    //CRUD methods
    
    //get top 30 grossing movies
    List<Movie> GetTop30GrossingMovies();
}