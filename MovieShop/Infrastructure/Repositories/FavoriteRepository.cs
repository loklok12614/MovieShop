using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public FavoriteRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<List<Favorite>> GetAllFavoritesByUserId(int userId)
    {
        var favorites = await _movieShopDbContext.Favorites
            .Include(f => f.Movie)
            .Where(f => f.UserId == userId)
            .ToListAsync();
        return favorites;
    }

    public Task<PagedResultSet<Favorite>> GetAllFavoritesByUserIdPagination(int userId, int pageSize = 30, int page = 1)
    {
        throw new NotImplementedException();
    }

    public Task<Favorite> AddFavorite(Favorite favorite)
    {
        throw new NotImplementedException();
    }

    public Task<int> RemoveFavorite(Favorite favorite)
    {
        throw new NotImplementedException();
    }
}