using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }
    public async Task<CastDetailsModel> GetCastDetails(int id)
    {
        var castDetails = await _castRepository.GetById(id);

        var castDetailsModel = new CastDetailsModel
        {
            Id = castDetails.Id,
            Name = castDetails.Name,
            Gender = castDetails.Gender,
            ProfilePath = castDetails.ProfilePath,
            TmdbUrl = castDetails.TmdbUrl,
        };

        foreach (var movie in castDetails.MoviesOfCast)
        {
            castDetailsModel.Movies.Add( new CastMovieModel
                {
                    Id = movie.MovieId, Character = movie.Character, Title = movie.Movie.Title, ReleaseDate = movie.Movie.ReleaseDate
                });
        }

        return castDetailsModel;
    }
}