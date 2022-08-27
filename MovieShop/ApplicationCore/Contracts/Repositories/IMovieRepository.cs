using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository
{
    //CRUD methods
    Movie GetById(int id);
    
    //get top 30 grossing movies
    List<Movie> GetTop30GrossingMovies();
}