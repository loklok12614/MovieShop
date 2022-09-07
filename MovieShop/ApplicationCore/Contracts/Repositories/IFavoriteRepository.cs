using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IFavoriteRepository
{
    Task<List<Favorite>> GetAllFavoritesByUserId(int userId);
    Task<PagedResultSet<Favorite>> GetAllFavoritesByUserIdPagination(int userId, int pageSize = 30, int page = 1);
    Task<Favorite> AddFavorite(Favorite favorite);
    Task<int> RemoveFavorite(Favorite favorite);
}