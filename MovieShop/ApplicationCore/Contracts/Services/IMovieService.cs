using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // service will typically expose the business functionality to the UI/client/controller

    Task< Tuple< List<MovieCardModel>, int>> GetAllMovies(int pageNumber, int pageSize);
    Task<List<MovieCardModel>> GetTop30GrossingMovies();
    Task<MovieDetailsModel> GetMovieDetails(int movieId);
}