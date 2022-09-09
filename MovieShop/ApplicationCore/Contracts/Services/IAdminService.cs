using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAdminService
{
    Task<int> CreateMovie(CreateMovieRequestModel model);
    Task<int> EditMovie(CreateMovieRequestModel model);
}