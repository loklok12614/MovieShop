using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllGenres();
}