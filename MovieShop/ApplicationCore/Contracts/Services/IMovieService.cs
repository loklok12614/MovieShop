using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // service will typically expose the business functionality to the UI/client/controller

    List<MovieCardModel> GetTop30GrossingMovies();
    MovieDetailsModel GetMovieDetails(int movieId);
}