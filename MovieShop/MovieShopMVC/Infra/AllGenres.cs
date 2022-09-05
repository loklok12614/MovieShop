using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace MovieShopMVC.Infra;

public class AllGenres : IAllGenres
{
    private readonly IGenreService _genreService;

    public AllGenres(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public Task<List<GenreModel>> Genres => _genreService.GetAllGenres();
}