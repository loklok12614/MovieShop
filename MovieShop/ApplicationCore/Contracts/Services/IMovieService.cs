using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // service will typically expose the business functionality to the UI/client/controller

    Task<PagedResultSet<MovieCardModel>> GetAllMovies(int pageSize = 30, int page = 1);
    Task<List<MovieCardModel>> GetTop30GrossingMovies();
    Task<MovieDetailsModel> GetMovieDetails(int movieId);
    Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genre, int pageSize = 30, int page = 1);
}