using ApplicationCore.Models;

namespace MovieShopMVC.Infra;

public interface IAllGenres
{
    Task<List<GenreModel>> Genres { get; }
}