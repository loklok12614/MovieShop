using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class AdminService : IAdminService
{
    public Task<int> CreateMovie(CreateMovieRequestModel model)
    {
        throw new NotImplementedException();
    }

    public Task<int> EditMovie(CreateMovieRequestModel model)
    {
        throw new NotImplementedException();
    }
}